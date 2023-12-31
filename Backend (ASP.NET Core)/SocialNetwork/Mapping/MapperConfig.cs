﻿using AutoMapper;
using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //Comment
            CreateMap<Comment, CommentRequestDTO>().ReverseMap();
            CreateMap<Comment, CommentResponseDTO>().ReverseMap();

            //Like
            CreateMap<Like, LikeRequestDTO>().ReverseMap();
            CreateMap<Like, LikeResponseDTO>().ReverseMap();

            //Label
            CreateMap<Label,  LabelRequestDTO>().ReverseMap();
            CreateMap<Label, LabelResponseDTO>().ReverseMap();

            //View
            //CreateMap<View, ViewRequestDTO>().ReverseMap();
            CreateMap<View, ViewResponseDTO>().ReverseMap();

            //Post
            CreateMap<Post, PostRequestDTO>().ReverseMap();
            CreateMap<Post, PostResponseDTO>().ReverseMap();
            CreateMap<PostResponseDTO, PostResponseDTOResp>().ReverseMap();

            //User
            CreateMap<User, UserRequestDTO>().ReverseMap();
            CreateMap<User, UserResponseDTO>().ReverseMap();

            //Attachment
            CreateMap<Attachment, AttachmentResponseDTO>().ReverseMap();

        }
    }
}
