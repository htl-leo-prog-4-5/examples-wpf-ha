using Survey.Repository.Entities;

namespace Survey.Repository.Context
{
    public static class SurveyDefaultData
    {
        public static void SurveySeed(SurveyContext context)
        {
			PollSeed(context);
		}
		private static void PollSeed(SurveyContext context)
		{
			var question1 = new Poll
			{
				Name = "Frage 1",
			};

			var question2 = new Poll
			{
				Name = "Frage 2",
			};

			var question3 = new Poll
			{
				Name = "Frage 3",
			};

			var question4 = new Poll
			{
				Name = "Frage 4",
			};

			var question5 = new Poll
			{
				Name = "Frage 5",
			};

			var question6 = new Poll
			{
				Name = "Frage 6",
			};


			var polls = new[]
			{
				question1,
				question2,
				question3,
				question4,
				question5,
				question6
			};

			var answers = new[]
			{
                new PollAnswer{ Poll=question1, Answer = "Antwort 1" },
				new PollAnswer{ Poll=question1, Answer = "Antwort 2" },
				new PollAnswer{ Poll=question1, Answer = "Antwort 3" },
				new PollAnswer{ Poll=question1, Answer = "Antwort 4" },
				new PollAnswer{ Poll=question1, Answer = "Antwort 5" },

				new PollAnswer{ Poll=question2, Answer = "Antwort 2/1" },
				new PollAnswer{ Poll=question2, Answer = "Antwort 2/2" },
				new PollAnswer{ Poll=question2, Answer = "Antwort 2/3" },
				new PollAnswer{ Poll=question2, Answer = "Antwort 2/4" },
				new PollAnswer{ Poll=question2, Answer = "Antwort 2/5" },
			};

			var votes = new[]
			{
				new PollVote{ Poll=question1, PollAnswer=answers[0], UserName = "Vote 1" },
				new PollVote{ Poll=question1, PollAnswer=answers[0], UserName = "Vote 2" },
				new PollVote{ Poll=question1, PollAnswer=answers[1], UserName = "Vote 3" },
				new PollVote{ Poll=question1, PollAnswer=answers[2], UserName = "Vote 4" },
				new PollVote{ Poll=question1, PollAnswer=answers[3], UserName = "Vote 5" },
			};


			context.Poll.AddRange(polls);
			context.PollAnswer.AddRange(answers);
			context.PollVote.AddRange(votes);
		}
	}
}
