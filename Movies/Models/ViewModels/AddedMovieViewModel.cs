using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models.ViewModels
{
    [Keyless]
    [NotMapped]
    public class AddedMovieViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Изображение")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Теги")]
        public string Categories { get; set; }
        [Display(Name = "Полезные ссылки")]
        public string Link { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        

        public (CommonMovieModel, SpecifiedMovieModel) CreateMovie(UserModel user = null)
        {
            CommonMovieModel commonMovie = new CommonMovieModel
            {
                Categories = Categories,
                Description = Description,
                Title = Title
            };
            using BinaryReader reader = new BinaryReader(Image.OpenReadStream());
            byte[] temp = reader.ReadBytes((int)Image.Length);
            commonMovie.Image = $"data:{Image.ContentType};base64, " + Convert.ToBase64String(temp);
            SpecifiedMovieModel specifiedMovie = new SpecifiedMovieModel
            {
                Comment = Comment,
                Link = Link,
                CommonMovie = commonMovie,
                User = user
            };
            return (commonMovie, specifiedMovie);
        }

    }
}
