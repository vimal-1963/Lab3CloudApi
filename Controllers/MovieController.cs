﻿using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models;
using MVCApplication.Services;

namespace MVCApplication.Controllers
{
    public class MovieController : Controller
    {
        DynamoOps dynamoOps = new DynamoOps();
        S3Ops s3Ops = new S3Ops();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/addMovie")]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost("/addNew")]
        public async Task<IActionResult> AddMovie(AddMovieModel movie)
        {
            Movie movieEntity = new Movie();
            String thumnailUrl =  await s3Ops.saveFiles(movie.Image);
            String movieUrl = await s3Ops.saveFiles(movie.Video);

            List<String> directors = new List<String>();
            directors.Add("jbj");
            MovieComment movieComment = new MovieComment();
            List<MovieComment> comments = new List<MovieComment>();
            comments.Add(movieComment);

            movieEntity.Title = movie.Title;
            movieEntity.URL = movieUrl;
            movieEntity.MovieImageUrl = thumnailUrl;
            movieEntity.Genre = movie.Genre;
            movieEntity.Rating = movie.Rating;
            movieEntity.Directors = directors;
            movieEntity.Comments = comments;
            movieEntity.UploadedUserId = "dfe";

            int movieId = await dynamoOps.GetSequence();
            movieEntity.MovieID = movieId.ToString();
            await dynamoOps.SaveNewMovie(movieEntity);

            return View("Signin");
        }
       
    }
}
