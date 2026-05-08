using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Data;
using SeniorCenterWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorCenterWebApp.Pages.Account
{
    public class HobbyTestModel : PageModel
    {
        private readonly DataContext _context;
        private readonly ILogger<HobbyTestModel> _logger;
       

        public HobbyTestModel(DataContext context, ILogger<HobbyTestModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Список вопросов и вариантов
        public List<QuestionModel> Questions { get; set; }

        // Результат теста
        [BindProperty]
        public string Result { get; set; }

        // Ответы пользователя
        [BindProperty]
        public List<string> Answers { get; set; }

        public class QuestionModel
        {
            public string QuestionText { get; set; }
            public List<AnswerModel> Answers { get; set; }
        }

        public class AnswerModel
        {
            public string AnswerText { get; set; }
            public string AnswerType { get; set; }
        }

        public async Task OnGet()
        {
            // Можно загрузить вопросы из базы, для примера статичный список:


            //Questions = new List<QuestionModel>
            Questions = new List<QuestionModel>
{
                new QuestionModel
    {
                QuestionText = "Что вам нравится делать в свободное время?",
                Answers = new List<AnswerModel>
        {
            new AnswerModel { AnswerText = "Танцевать", AnswerType = "Танцы" },
            new AnswerModel { AnswerText = "Петь", AnswerType = "Пение" },
            new AnswerModel { AnswerText = "Создавать что-то руками", AnswerType = "Рукоделие" },
            new AnswerModel { AnswerText = "Рисовать или творить", AnswerType = "Рисование" }
        }
    },
    new QuestionModel
    {
        QuestionText = "Что вызывает у вас больше удовольствия?",
        Answers = new List<AnswerModel>
        {
            new AnswerModel { AnswerText = "Выступать на сцене", AnswerType = "Пение" },
            new AnswerModel { AnswerText = "Учиться новым танцам", AnswerType = "Танцы" },
            new AnswerModel { AnswerText = "Вязать или шить", AnswerType = "Рукоделие" },
            new AnswerModel { AnswerText = "Рисовать картины", AnswerType = "Рисование" }
        }
    },
    new QuestionModel
    {
        QuestionText = "Что вам ближе?",
        Answers = new List<AnswerModel>
        {
            new AnswerModel { AnswerText = "Учиться новым движениям", AnswerType = "Танцы" },
            new AnswerModel { AnswerText = "Петь любимые песни", AnswerType = "Пение" },
            new AnswerModel { AnswerText = "Создавать украшения или сувениры", AnswerType = "Рукоделие" },
            new AnswerModel { AnswerText = "Создавать красивые картинки", AnswerType = "Рисование" }
        }
    },
    new QuestionModel
    {
        QuestionText = "Что вы предпочитаете делать в компании?",
        Answers = new List<AnswerModel>
        {
            new AnswerModel { AnswerText = "Танцевать вместе", AnswerType = "Танцы" },
            new AnswerModel { AnswerText = "Петь караоке", AnswerType = "Пение" },
            new AnswerModel { AnswerText = "Вязать или шить с друзьями", AnswerType = "Рукоделие" },
            new AnswerModel { AnswerText = "Совместное рисование", AnswerType = "Рисование" }
        }
    },
    new QuestionModel
    {
        QuestionText = "Что вам нравится больше всего на уроках?",
        Answers = new List<AnswerModel>
        {
            new AnswerModel { AnswerText = "Танцевальные движения", AnswerType = "Танцы" },
            new AnswerModel { AnswerText = "Пение под музыку", AnswerType = "Пение" },
            new AnswerModel { AnswerText = "Работа с тканью или бумагой", AnswerType = "Рукоделие" },
            new AnswerModel { AnswerText = "Рисование и живопись", AnswerType = "Рисование" }
        }
    },
    new QuestionModel
    {
        QuestionText = "Что вам приносит наибольшее удовлетворение?",
        Answers = new List<AnswerModel>
        {
            new AnswerModel { AnswerText = "Выступать перед публикой", AnswerType = "Пение" },
            new AnswerModel { AnswerText = "Создавать что-то руками", AnswerType = "Рукоделие" },
            new AnswerModel { AnswerText = "Учиться танцевать", AnswerType = "Танцы" },
            new AnswerModel { AnswerText = "Рисовать и создавать искусство", AnswerType = "Рисование" }
        }
    },
    new QuestionModel
    {
        QuestionText = "Что бы вы хотели попробовать?",
        Answers = new List<AnswerModel>
        {
            new AnswerModel { AnswerText = "Записаться в хор", AnswerType = "Пение" },
            new AnswerModel { AnswerText = "Научиться новым танцам", AnswerType = "Танцы" },
            new AnswerModel { AnswerText = "Освоить технику вышивки", AnswerType = "Рукоделие" },
            new AnswerModel { AnswerText = "Начать рисовать картины", AnswerType = "Рисование" }
        }
    }
};
           
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var stopwatch = Stopwatch.StartNew();

            await OnGet(); // чтобы вернуть вопросы при ошибках

            if (Answers == null || Answers.Count != Questions.Count)
            {
                ModelState.AddModelError(string.Empty, "Пожалуйста, ответьте на все вопросы.");
                return Page();
            }

            // Подсчет частоты типов ответов
            var hobbyScores = new Dictionary<string, int>();

            foreach (var answerType in Answers)
            {
                if (hobbyScores.ContainsKey(answerType))
                    hobbyScores[answerType]++;
                else
                    hobbyScores[answerType] = 1;
            }

            // Находим тип с максимальным счетом
            var maxScore = hobbyScores.Values.Max();
            var bestHobbies = hobbyScores.Where(kvp => kvp.Value == maxScore).Select(kvp => kvp.Key).ToList();

            // Если несколько, выбираем первый
            Result = bestHobbies.First();

            // Получаем текущего пользователя
            var username = User.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user != null)
                {
                    var testResult = new TestResult
                    {
                        UserId = user.Id,
                        HobbyResult = Result,
                        DateTaken = DateTime.UtcNow
                    };

                    _context.TestResults.Add(testResult);
                    await _context.SaveChangesAsync();

                    //await Task.Delay(Random.Shared.Next(200, 300));

                    stopwatch.Stop();

                    _logger.LogInformation(
                        "User '{Username}' hobby test saved in {Time} ms",
                        username,
                        stopwatch.ElapsedMilliseconds
                    );
                }
            }

            return Page();
        }
    }
}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using SeniorCenterWebApp.Data;
//using SeniorCenterWebApp.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;

//namespace SeniorCenterWebApp.Pages.Account
//{
//    public class HobbyTestModel : PageModel
//    {
//        private readonly DataContext _context;

//        public HobbyTestModel(DataContext context)
//        {
//            _context = context;
//        }

//        // Список вопросов и вариантов
//        public List<QuestionModel> Questions { get; set; }

//        // Результат теста
//        [BindProperty]
//        public string Result { get; set; }

//        // Ответы пользователя
//        [BindProperty]
//        public List<string> Answers { get; set; }

//        public class QuestionModel
//        {
//            public string QuestionText { get; set; }
//            public List<AnswerModel> Answers { get; set; }
//        }

//        public class AnswerModel
//        {
//            public string AnswerText { get; set; }
//            public string AnswerType { get; set; }
//        }




//        public async Task OnGet()
//        {
//            // Пример вопросов (можно брать из базы)
//            Questions = new List<QuestionModel>
//            {
//                new QuestionModel
//                {
//                    QuestionText = "Какое занятие вам больше нравится?",
//                    Answers = new List<AnswerModel>
//                    {
//                        new AnswerModel { AnswerText = "Рисовать", AnswerType = "Art" },
//                        new AnswerModel { AnswerText = "Спорт", AnswerType = "Sport" },
//                        new AnswerModel { AnswerText = "Чтение", AnswerType = "Reading" }
//                    }
//                },
//                new QuestionModel
//                {
//                    QuestionText = "Что вы предпочитаете делать в свободное время?",
//                    Answers = new List<AnswerModel>
//                    {
//                        new AnswerModel { AnswerText = "Играть в игры", AnswerType = "Gaming" },
//                        new AnswerModel { AnswerText = "Прогулки на природе", AnswerType = "Outdoor" },
//                        new AnswerModel { AnswerText = "Слушать музыку", AnswerType = "Music" }
//                    }
//                }
//            };
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            // Загрузка вопросов для рендера страницы в случае ошибок
//            await OnGet();

//            if (Answers == null || Answers.Count != Questions.Count)
//            {
//                ModelState.AddModelError(string.Empty, "Пожалуйста, ответьте на все вопросы.");
//                return Page();
//            }

//            // Простейший алгоритм: берём наиболее часто выбранный тип
//            var resultType = Answers
//                .GroupBy(a => a)
//                .OrderByDescending(g => g.Count())
//                .First()
//                .Key;

//            Result = resultType;

//            // Получаем текущее имя пользователя из Cookie
//            var username = User.Identity?.Name;

//            if (!string.IsNullOrEmpty(username))
//            {
//                var user = await _context.Users
//                    .FirstOrDefaultAsync(u => u.Username == username);

//                if (user != null)
//                {
//                    // Сохраняем результат в базе, например, в отдельной таблице TestResults
//                    var testResult = new TestResult
//                    {
//                        UserId = user.Id,
//                        HobbyResult = Result,
//                        DateTaken = DateTime.UtcNow
//                    };

//                    _context.TestResults.Add(testResult);
//                    await _context.SaveChangesAsync();
//                }
//            }

//            return Page();
//        }
//    }
//}