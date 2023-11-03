using BusinessLayer.Services.Implementations;
using DataLayer.Data;
using DataLayer.Models;

namespace ControllerLayer
{
	public class Seed
	{
		private readonly DataContext dataContext;

		public Seed(DataContext context)
		{
			dataContext = context;
		}

		public void SeedDataContext()
		{
			if (!dataContext.Movies.Any())
			{
				var japan = new Country { Name = "Japan" };
				var usa = new Country { Name = "United States" };
				var uk = new Country { Name = "United Kingdom" };
				var russia = new Country { Name = "Russia" };

				var nolan = new Director { Name = "Christopher Nolan" };
				var fincher = new Director { Name = "David Fincher" };
				var scorsese = new Director { Name = "Martin Scorsese" };
				var columbus = new Director { Name = "Cris Columbus" };
				var cameron = new Director { Name = "James Cameron" };
				var balabanov = new Director { Name = "Alexei Balabanov" };
				var shinkai = new Director { Name = "Makoto Shinkai" };

				var oppenheimer = new Movie
				{
					Title = "Oppenheimer",
					Year = 2023,
					Director = nolan,
					Description = "The story of American scientist, J. Robert Oppenheimer, and his role in the development of the atomic bomb.",
					Country = usa,
					ImagePath = "oppenheimer.jpg"
				};
				var fightclub = new Movie
				{
					Title = "Fight Club",
					Year = 1999,
					Director = fincher,
					Description = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.",
					Country = usa,
					ImagePath = "fight_club.jpg"
				};
				var shutterisland = new Movie
				{
					Title = "Shutter Island",
					Year = 2010,
					Director = scorsese,
					Description = "Teddy Daniels and Chuck Aule, two US marshals, are sent to an asylum on a remote island in order to investigate the disappearance of a patient, where Teddy uncovers a shocking truth about the place.",
					Country = usa,
					ImagePath = "shutter_island.jpg"
				};
				var harrypotter = new Movie
				{
					Title = "Harry Potter and the Philosopher's Stone",
					Year = 2001,
					Director = columbus,
					Description = "An orphaned boy enrolls in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.",
					Country = uk,
					ImagePath = "harry_potter_and_the_philosophers_stone.jpg"
				};
				var titanic = new Movie
				{
					Title = "Titanic",
					Year = 1997,
					Director = cameron,
					Description = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.",
					Country = usa,
					ImagePath = "titanic.jpg"
				};
				var prestige = new Movie
				{
					Title = "The Prestige",
					Year = 2006,
					Director = nolan,
					Description = "After a tragic accident, two stage magicians in 1890s London engage in a battle to create the ultimate illusion while sacrificing everything they have to outwit each other.",
					Country = usa,
					ImagePath = "the_prestige.jpg"
				};
				var cargo200 = new Movie
				{
					Title = "Cargo 200",
					Year = 2007,
					Director = balabanov,
					Description = "A young woman is taken hostage by a police officer gone mad.",
					Country = russia,
					ImagePath = "cargo_200.jpg"
				};
				var yourname = new Movie
				{
					Title = "Your Name",
					Year = 2016,
					Director = shinkai,
					Description = "Two teenagers share a profound, magical connection upon discovering they are swapping bodies. Things manage to become even more complicated when the boy and girl decide to meet in person.",
					Country = japan,
					ImagePath = "your_name.jpg"
				};

				var passwordService = new PasswordService();
				var user1 = new User { Username = "xxx888xx", Password = passwordService.HashPassword("00000000") };
				var user2 = new User { Username = "dmitriy", Password = passwordService.HashPassword("12345678") };
				var user3 = new User { Username = "Joe Biden", Password = passwordService.HashPassword("qwertyuiop") };
				var user4 = new User { Username = "Sava", Password = passwordService.HashPassword("12345678") };
				var user5 = new User { Username = "John Doe", Password = passwordService.HashPassword("12345678") };
				var user6 = new User { Username = "Anatoly", Password = passwordService.HashPassword("98765432") };
				var admin = new User
				{
					IsAdmin = true,
					Username = "Administrator",
					Password = passwordService.HashPassword("adminpassword")
				};

				var review11 = new Review()
				{
					User = user1,
					Movie = oppenheimer,
					Text = "The best movie of 2023!!!",
					Rating = 10,
				};
				var review12 = new Review()
				{
					User = user1,
					Movie = fightclub,
					Text = "The best movie of 1999!!!",
					Rating = 10,
				};
				var review13 = new Review()
				{
					User = user1,
					Movie = shutterisland,
					Text = "The best movie of 2010!!!",
					Rating = 10,
				};
				var review14 = new Review()
				{
					User = user1,
					Movie = harrypotter,
					Text = "The best movie of 2001!!!",
					Rating = 10,
				};
				var review15 = new Review()
				{
					User = user1,
					Movie = titanic,
					Text = "The best movie of 1997!!!",
					Rating = 10,
				};
				var review16 = new Review()
				{
					User = user1,
					Movie = prestige,
					Text = "The best movie of 2006!!!",
					Rating = 10,
				};
				var review17 = new Review()
				{
					User = user1,
					Movie = cargo200,
					Text = "The best movie of 2007!!!",
					Rating = 10,
				};
				var review18 = new Review()
				{
					User = user1,
					Movie = yourname,
					Text = "The best movie of 2016!!!",
					Rating = 10,
				};

				var review21 = new Review()
				{
					User = user2,
					Movie = oppenheimer,
					Text = "wow nice movie 10/10",
					Rating = 10,
				};
				var review22 = new Review()
				{
					User = user2,
					Movie = fightclub,
					Text = "good",
					Rating = 9,
				};
				var review23 = new Review()
				{
					User = user2,
					Movie = shutterisland,
					Text = "This is my favourite movie by this director.",
					Rating = 10,
				};
				var review24 = new Review()
				{
					User = user2,
					Movie = harrypotter,
					Text = "i like this one less than other harry potter movies",
					Rating = 7,
				};
				var review25 = new Review()
				{
					User = user2,
					Movie = titanic,
					Text = "",
					Rating = 8,
				};
				var review26 = new Review()
				{
					User = user2,
					Movie = prestige,
					Text = "весёлый фильм о двух братьях",
					Rating = 8,
				};
				var review27 = new Review()
				{
					User = user2,
					Movie = cargo200,
					Text = "мне нравится.",
					Rating = 10,
				};
				var review28 = new Review()
				{
					User = user2,
					Movie = yourname,
					Text = "не оч",
					Rating = 7,
				};

				var review31 = new Review()
				{
					User = user3,
					Movie = oppenheimer,
					Text = "Nolan at his best!\r\nAs VFX and special effects take over the traditional " +
							"filmmaking methods, Nolan is among the remaining few directors who still builds " +
							"grandiose true-to-life sets and reflects cinematic setpieces by filming them " +
							"instead of digitising them. Oppenheimer is thus a culmination of Nolan's cinematic " +
							"genius combined with an incredible story that changed the world in more ways than " +
							"one. It's incredibly intimate and divisive, with the onus of it's justification " +
							"being put on the audience instead of the narrator.\r\n\r\nThe cast is just as " +
							"incredible as you would expect it to be, and the screenplay flows naturally, with " +
							"a breathtaking score that justifiably draws parallels from Hans Zimmer's profound " +
							"work in Interstellar. Nolan balances the intimacy between the characters while " +
							"simultaneously juxtaposing it with some of the most impactful scenes ever shown on " +
							"the big screen. The movie's runtime takes it's time in setting up it's pieces, " +
							"with the finale leaving you utterly spellbound at the sheer magnitude and scale " +
							"of the events transpiring right in front of your eyes.\r\n\r\nLastly, for those " +
							"who want their daily dosage of immediate dopamine and faster pacing in the theatre " +
							"instead of experiencing a meticulously guided journey, you could wait a few more " +
							"months for yet another copy paste Fast and Furious flick or a Marvel movie laden " +
							"with green backdrops and fan-service.\r\n\r\nBecause this isn't a film or a flick, " +
							"this is Cinema.",
					Rating = 10,
				};
				var review33 = new Review()
				{
					User = user3,
					Movie = shutterisland,
					Text = "Of all the movies in theatres to see, this is worth your time\r\nI just saw " +
					"Shutter Island this evening, just prior to its American release. I have to say this " +
					"film was full of intrigue. Prior to viewing this film I had built a preconceived " +
					"notion of what this thriller was going to be like because I was fooled yet again " +
					"by good marketing when watching the trailer. This is probably not the movie for your " +
					"average film-goer who wants an easy plot line to follow and little thought required. " +
					"This movie does challenge the viewer physchologically and definitely holds your " +
					"attention all the way through. For someone who was never much of a Leonardo fan, " +
					"his performance is brilliant, so much range to his character. In fact all of the " +
					"acting in this film is excellent. The directing is probably the best quality to this " +
					"film. I always enjoy watching a film that is as unpredicatable as this film and " +
					"where the director has turned the plot line on to his viewer.",
					Rating = 10,
				};
				var review36 = new Review()
				{
					User = user3,
					Movie = prestige,
					Text = "Outstanding acting performances worth price of admission\r\nI went to " +
					"see a critics preview of The Prestige this afternoon and to my surprise I found " +
					"the film to be one of the best I have seen all year so far, and that writers " +
					"can come up with an excellent script it they would only try a little harder. " +
					"The acting performances by Hugh Jackman, Christian Bale and Michael Caine were " +
					"the best I have see in a long while. The only objection I had to the film was " +
					"that it was a little long, but once you leave the theater you will discuss the " +
					"film and it many twists and turns. My wife and myself discussed it all the way " +
					"home from the movie theater. This is a winner and should be up for some academy " +
					"award statues, and my recommendation is go see this as soon as you can, you " +
					"will not be disappointed.",
					Rating = 9,
				};
				var review38 = new Review()
				{
					User = user3,
					Movie = yourname,
					Text = "The Japanese Sure Know How To Unleash\r\nBeauty, Mystery and Creativity, don't they?\r\n\r\n" +
					"I am scared for the day Jar Jar Abrams and Kathleen Kennedy get hold of this and make the hero " +
					"a Disney princess and bore all our pants off.\r\n\r\nbefore Disney gives Kimi No Na Wa the Ghost " +
					"In The Shell treatment get hold of this original and beautiful film. I guarantee you the girl is " +
					"not Melissa McCarthy and doesn't have body issues.",
					Rating = 10,
				};

				var review41 = new Review()
				{
					User = user4,
					Movie = oppenheimer,
					Text = "заставляет задуматься....",
					Rating = 10,
				};
				var review42 = new Review()
				{
					User = user4,
					Movie = fightclub,
					Text = "очень плохой фильм",
					Rating = 5,
				};
				var review43 = new Review()
				{
					User = user4,
					Movie = shutterisland,
					Text = "сложна сложна сложна",
					Rating = 8,
				};
				var review44 = new Review()
				{
					User = user4,
					Movie = harrypotter,
					Text = "",
					Rating = 7,
				};
				var review45 = new Review()
				{
					User = user4,
					Movie = titanic,
					Text = "сопливая нудятина для подростков",
					Rating = 6,
				};
				var review47 = new Review()
				{
					User = user4,
					Movie = cargo200,
					Text = "тупое российское кинцо пропагандирующее насилие. больше единицы не заслуживает",
					Rating = 1,
				};

				var review56 = new Review()
				{
					User = user5,
					Movie = prestige,
					Text = "that's interesting",
					Rating = 8,
				};
				var review58 = new Review()
				{
					User = user5,
					Movie = yourname,
					Text = "i love anime",
					Rating = 7,
				};

				user1.Reviews = new List<Review>()
				{
					review11, review12, review13, review14, review15, review16, review17, review18
				};
				user2.Reviews = new List<Review>()
				{
					review21, review22, review23, review24, review25, review26, review27, review28
				};
				user3.Reviews = new List<Review>()
				{
					review31, review33, review36, review38
				};
				user4.Reviews = new List<Review>()
				{
					review41, review42, review43, review44, review45, review47
				};
				user5.Reviews = new List<Review>()
				{
					review56, review58
				};

				oppenheimer.Reviews = new List<Review>()
				{
					review11, review21, review31, review41
				};
				fightclub.Reviews = new List<Review>
				{
					review12, review22, review42
				};
				shutterisland.Reviews = new List<Review>
				{
					review13, review23, review33, review43
				};
				harrypotter.Reviews = new List<Review>()
				{
					review14, review24, review44
				};
				titanic.Reviews = new List<Review>
				{
					review15, review25, review45
				};
				prestige.Reviews = new List<Review>()
				{
					review16, review26, review36, review56
				};
				cargo200.Reviews = new List<Review>()
				{
					review17, review27, review47
				};
				yourname.Reviews = new List<Review>()
				{
					review18, review28, review38, review58
				};
				oppenheimer.ReviewsCount = oppenheimer.Reviews.Count;
				fightclub.ReviewsCount = fightclub.Reviews.Count;
				shutterisland.ReviewsCount = shutterisland.Reviews.Count;
				harrypotter.ReviewsCount = harrypotter.Reviews.Count;
				titanic.ReviewsCount = titanic.Reviews.Count;
				prestige.ReviewsCount = prestige.Reviews.Count;
				cargo200.ReviewsCount = cargo200.Reviews.Count;
				yourname.ReviewsCount = yourname.Reviews.Count;

				nolan.Movies = new List<Movie>() { oppenheimer, prestige };
				fincher.Movies = new List<Movie>() { fightclub };
				scorsese.Movies = new List<Movie>() { shutterisland };
				columbus.Movies = new List<Movie>() { harrypotter };
				cameron.Movies = new List<Movie>() { titanic };
				balabanov.Movies = new List<Movie>() { cargo200 };
				shinkai.Movies = new List<Movie>() { yourname };

				var bio = new Genre
				{
					Name = "Biography",
					Movies = new List<Movie>()
					{
						oppenheimer
					}
				};
				var drama = new Genre
				{
					Name = "Drama",
					Movies = new List<Movie>()
					{
						oppenheimer, fightclub, titanic, prestige, cargo200, yourname
					}
				};
				var history = new Genre
				{
					Name = "History",
					Movies = new List<Movie>()
					{
						oppenheimer,
					}
				};
				var mystery = new Genre
				{
					Name = "Mystery",
					Movies = new List<Movie>()
					{
						shutterisland, prestige
					}
				};
				var thriller = new Genre
				{
					Name = "Thriller",
					Movies = new List<Movie>()
					{
						shutterisland, cargo200
					}
				};
				var adventure = new Genre
				{
					Name = "Adventure",
					Movies = new List<Movie>()
					{
						harrypotter
					}
				};
				var fantasy = new Genre
				{
					Name = "Fantasy",
					Movies = new List<Movie>()
					{
						harrypotter, yourname
					}
				};
				var romance = new Genre
				{
					Name = "Romance",
					Movies = new List<Movie>()
					{
						titanic
					}
				};
				var scifi = new Genre
				{
					Name = "Sci-fi",
					Movies = new List<Movie>()
					{
						prestige
					}
				};
				var crime = new Genre
				{
					Name = "Crime",
					Movies = new List<Movie>()
					{
						cargo200
					}
				};
				var animation = new Genre
				{
					Name = "Animation",
					Movies = new List<Movie>()
					{
						yourname
					}
				};

				oppenheimer.Genres = new List<Genre>() { bio, drama, history };
				fightclub.Genres = new List<Genre>() { drama };
				shutterisland.Genres = new List<Genre>() { mystery, thriller };
				harrypotter.Genres = new List<Genre>() { adventure, fantasy };
				titanic.Genres = new List<Genre>() { drama, romance };
				prestige.Genres = new List<Genre>() { drama, mystery, scifi };
				cargo200.Genres = new List<Genre>() { crime, drama, thriller };
				yourname.Genres = new List<Genre>() { animation, drama, fantasy };

				var movies = new List<Movie>()
				{
					oppenheimer, fightclub, shutterisland, harrypotter, titanic, prestige, cargo200, yourname
				};

				foreach (var movie in movies)
				{
					movie.Rating = ReviewService.CalculateMovieRating(movie, 0);
				}

				dataContext.Movies.AddRange(movies);
				dataContext.Users.Add(user6);
				dataContext.Users.Add(admin);
				dataContext.SaveChanges();
			}
		}
	}
}
