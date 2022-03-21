using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Converters
{
    public static class StudentConverter
    {
        public static StudentWrapper ToWrapper(this Student model)//konwerter z student na studentwrapper
        {
            return new StudentWrapper
            {
                Id = model.Id,//konwertuje Id (StudentWrapper) z Id (Student)
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name
                },
                Math = string.Join(", ", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.Math)//zwraca wszystkie obiekty ratings o podanym SubjectId
                    .Select(y => y.Rate)),//wybiera tylko oceny
                Technology = string.Join(", ", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.Technology)
                    .Select(y => y.Rate)),
                Physics = string.Join(", ", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.Physics)
                    .Select(y => y.Rate)),
                PolishLang = string.Join(", ", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.PolishLang)
                    .Select(y => y.Rate)),
                EnglishLang = string.Join(", ", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.EnglishLang)
                    .Select(y => y.Rate)),
            };
        }

        public static Student ToDao(this StudentWrapper model)
        {
            return new Student()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                GroupId = model.Group.Id
            };
        }

        public static List<Rating> ToRatingDao(this StudentWrapper model)//konwersja ocen ze stdentwrapper
        {
            var ratings = new List<Rating>();

            if (!string.IsNullOrWhiteSpace(model.Math))
                model.Math.Split(',').ToList().ForEach(x =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Math
                        }));

            if (!string.IsNullOrWhiteSpace(model.Physics))
                model.Physics.Split(',').ToList().ForEach(x =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Physics
                        }));

            if (!string.IsNullOrWhiteSpace(model.Technology))
                model.Technology.Split(',').ToList().ForEach(x =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Technology
                        }));

            if (!string.IsNullOrWhiteSpace(model.PolishLang))
                model.PolishLang.Split(',').ToList().ForEach(x =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.PolishLang
                        }));

            if (!string.IsNullOrWhiteSpace(model.EnglishLang))
                model.EnglishLang.Split(',').ToList().ForEach(x =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(x),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.EnglishLang
                        }));


            return ratings;
        }
    }
}
