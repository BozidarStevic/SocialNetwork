using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services.IServices;
using static System.Net.Mime.MediaTypeNames;
using SocialNetwork.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.Debugger.Contracts.EditAndContinue;

namespace SocialNetwork.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IViewRepository _viewRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostService(IPostRepository postRepository,
            ILabelRepository labelRepository,
            IMapper mapper, UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment,
            IViewRepository viewRepository,
            IAttachmentRepository attachmentRepository)
        {
            _postRepository = postRepository;
            _labelRepository = labelRepository;
            _mapper = mapper;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _viewRepository = viewRepository;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<PostResponseDTO> GetPostByIdAsync(int id)
        {
            Post post = await _postRepository.GetPostByIdAsync(id);
            return _mapper.Map<PostResponseDTO>(post);
        }

        public async Task<PostResponseDTO> CreatePostAsync(PostRequestDTO postRequestDTO, User user)
        {
            var post = _mapper.Map<Post>(postRequestDTO);
            
            if (postRequestDTO.AttachemtsFiles != null)
            {
                AddAttachmentsToPost(post, postRequestDTO.AttachemtsFiles);
            }
            
            post.TimePosted = DateTime.Now;
            post.UserId = user.Id;
            post.User = user;

            if (postRequestDTO.labelsIds != null)
            {
                List<Label> labels = new List<Label>();
                foreach (var labelId in postRequestDTO.labelsIds)
                {
                    var label = await _labelRepository.GetLabelByIdAsync(labelId);
                    if (label != null)
                    {
                        labels.Add(label);
                    }
                }
                post.Labels = labels;
            }
            
            var createdPost = await _postRepository.CreatePostAsync(post);
            return _mapper.Map<PostResponseDTO>(createdPost);
        }

        private async Task<Post> AddAttachmentsToPost(Post post, IFormFileCollection AttachemtsFiles)
        {
            string folder = "posts/attachments/";
            
            if (post.Attachments == null)
            {
                post.Attachments = new List<Attachment>();
            }

            foreach (var file in AttachemtsFiles)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                Attachment attachment = null;
                if (IsImageFile(fileExtension))
                {
                    attachment = new Attachment()
                    {
                        Name = file.FileName,
                        Url = await UploadFile(folder, file),
                        Type = "image"
                    };
                }
                else if (IsVideoFile(fileExtension))
                {
                    attachment = new Attachment()
                    {
                        Name = file.FileName,
                        Url = await UploadFile(folder, file),
                        Type = "video"
                    };
                }

                if (attachment != null)
                {
                    post.Attachments.Add(attachment);
                }
            }
            return post;
        }

        private bool IsImageFile(string fileExtension)
        {
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif" };

            return imageExtensions.Contains(fileExtension.ToLower());
        }

        private bool IsVideoFile(string fileExtension)
        {
            string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".mov" };

            return videoExtensions.Contains(fileExtension.ToLower());
        }

        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await using (FileStream fileStream = new FileStream(serverFolder, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                // Resursi će biti automatski oslobođeni nakon što se using blok završi.
            }

            return "/" + folderPath;
        }

        public async Task<List<PostResponseDTO>> GetAllPostDTOsSortedByDateTimeAsync(User user)
        {
            var posts = await _postRepository.GetAllPostsSortedByDateTimeAsync();
            foreach (var post in posts)
            {
                var viewExist = await _viewRepository.findViewByUserAndPostAsync(post, user);
                if (viewExist == null)
                {
                    await _viewRepository.CreateViewAsync(post, user);
                }
            }

            return _mapper.Map<List<PostResponseDTO>>(posts);
        }

        public async Task<PostResponseDTO> UpdatePostAsync(string currentUserId, int postId, UpdatePostDTO updatePostDTO)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);

            if (post == null)
            {
                throw new NotFoundException("Post nije pronađen.");
            }

            if (post.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Nemate dozvolu da ažurirate ovaj post.");
            }

            if (updatePostDTO.Text != null)
            {
                post.Text = updatePostDTO.Text;
            }
            if (updatePostDTO.Location != null)
            {
                post.Location = updatePostDTO.Location;
            }
            if (updatePostDTO.LabelsIdsForDelete != null && post.Labels != null)
            {
                foreach (var labelIdForDelete in updatePostDTO.LabelsIdsForDelete)
                {
                    List<Label> postLabelsCopy = new List<Label>(post.Labels);
                    foreach (var label in postLabelsCopy)
                    {
                        if (label.Id == labelIdForDelete)
                        {
                            post.Labels.Remove(label);
                        }
                    }
                }
            }
            if (updatePostDTO.NewLabelsIds != null)
            {
                foreach (var newLabelId in updatePostDTO.NewLabelsIds)
                {
                    var label = await _labelRepository.GetLabelByIdAsync(newLabelId);
                    if (post.Labels == null)
                    {
                        post.Labels = new List<Label>();
                    }
                    post.Labels.Add(label);
                }
            }

            if (updatePostDTO.AttachmentsIdsForDelete != null && post.Attachments != null)
            {
                foreach (var attachmentIdForDelete in updatePostDTO.AttachmentsIdsForDelete)
                {
                    List<Attachment> postAttachmentsCopy = new List<Attachment>(post.Attachments);
                    foreach (var attachment in postAttachmentsCopy)
                    {
                        if (attachment.Id == attachmentIdForDelete)
                        {
                            post.Attachments.Remove(attachment);
                            await _attachmentRepository.DeleteAttachmentAsync(attachmentIdForDelete);

                            if (attachment.Url != null)
                            {
                                try
                                {
                                    string relativePath = attachment.Url.TrimStart('/');
                                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);
                                    File.Delete(serverFolder);
                                    Console.WriteLine("Fajl je uspešno obrisan.");
                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine("Došlo je do IO greške prilikom brisanja fajla: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            if (updatePostDTO.AttachemtsFiles != null)
            {
                if (post.Attachments != null)
                {
                    if ((updatePostDTO.AttachemtsFiles.Count() + post.Attachments.Count()) <= 4)
                    {
                        await AddAttachmentsToPost(post, updatePostDTO.AttachemtsFiles);
                    }
                    else
                    {
                        throw new BadHttpRequestException("Post moze imati max 4 attachment-a!");
                    }
                }
                else if (updatePostDTO.AttachemtsFiles.Count() <= 4)
                {
                    AddAttachmentsToPost(post, updatePostDTO.AttachemtsFiles);
                }
                else
                {
                    throw new BadHttpRequestException("Post moze imati max 4 attachment-a!");
                }
            }

            post = await _postRepository.UpdatePostAsync(post);
            return _mapper.Map<PostResponseDTO>(post);
        }

        public async Task<bool> DeletePostAsync(int postId, string userId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);

            if (post != null && post.UserId == userId)
            {
                return await _postRepository.DeletePostAsync(post);
            }

            return false;
        }
    }
}
