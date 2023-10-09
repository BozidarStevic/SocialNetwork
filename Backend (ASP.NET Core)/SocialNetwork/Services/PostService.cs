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

namespace SocialNetwork.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostService(IPostRepository postRepository,
            ILabelRepository labelRepository,
            IMapper mapper, UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _postRepository = postRepository;
            _labelRepository = labelRepository;
            _mapper = mapper;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PostResponseDTO> GetPostByIdAsync(int id)
        {
            Post post = await _postRepository.GetPostByIdAsync(id);
            return _mapper.Map<PostResponseDTO>(post);
        }

        public async Task<PostResponseDTO> CreatePost(PostRequestDTO postRequestDTO, User user)
        {
            var post = _mapper.Map<Post>(postRequestDTO);
            
            if (postRequestDTO.AttachemtsFiles != null)
            {
                string folder = "posts/attachments/";

                post.Attachments = new List<Attachment>();

                foreach (var file in postRequestDTO.AttachemtsFiles)
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
            
            var createdPost = await _postRepository.CreatePost(post);
            return _mapper.Map<PostResponseDTO>(createdPost);
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

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

    }
}
