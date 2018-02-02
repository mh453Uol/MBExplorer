using AutoMapper;
using MBExplorer.Core.Dtos;
using MBExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBExplorer.Utilities.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Bookmark, BookmarkDto>()
                .ForMember(b => b.Text, b => b.MapFrom(f => f.Name))
                .ForMember(b => b.Id, b => b.MapFrom(f => f.Id))
                .ForMember(b => b.Parent, b => b.MapFrom(f => f.ParentId == null ? "#" : f.ParentId.ToString()))
                .ForMember(b => b.Type, b => b.MapFrom(f => f.GetType().Name))
                .ForMember(b => b.IsReadOnly, b => b.MapFrom(f => f.ReadOnly));

            CreateMap<Bookmark, FolderDto>()
                .ForMember(f => f.Folder, src => src.MapFrom(f => f.Name))
                .ForMember(f => f.SubFolder, src => src.MapFrom(f => f.Children));
        }
    }
}
