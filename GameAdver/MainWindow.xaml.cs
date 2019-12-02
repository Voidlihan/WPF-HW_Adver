using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameAdver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddGames();
            listBoxGames.ItemsSource = Games();
        }

        private static List<Game> Games()
        {
            using (Context context = new Context("Server=BorisHome\\Boris;Database=GameAd;Trusted_Connection=True;"))
            {
                return context.Games.ToList();
            }
        }

        private static void AddGames()
        {
            using (Context context = new Context("Server=BorisHome\\Boris;Database=GameAd;Trusted_Connection=True;"))
            {
                Game game1 = new Game
                {
                    MainImage = "https://images-na.ssl-images-amazon.com/images/I/510RHE9vePL.jpg",
                    Developer = "CD Projekt Red",
                    Name = "The Witcher 3: Wild Hunt",
                    Price = 6900,
                    ReleaseDate = new DateTime(2015, 5, 18),
                    Rating = 9.3,
                    Genre = "action/RPG",
                    Description = $"«Ведьмак 3: Дикая Охота» — это сюжетная ролевая игра с открытым миром. Её действие разворачивается в поразительной волшебной вселенной, и любое ваше решение может повлечь за собой серьёзные последствия. Вы играете за профессионального охотника на монстров Геральта из Ривии, которому поручено найти Дитя предназначения в огромном мире, полном торговых городов, пиратских островов, опасных горных перевалов и заброшенных пещер." +
                    $"\nОСНОВНЫЕ ОСОБЕННОСТИ" +
                    $"\nИГРАЙТЕ ЗА ПРОФЕССИОНАЛЬНОГО УБИЙЦУ ЧУДОВИЩ" +
                    $"\nВедьмаки с детства готовятся к борьбе с чудовищами.Благодаря мутациям и усердным тренировкам ведьмаки обретают сверхчеловеческие способности," +
                    $"\nсилу и скорость реакции.Только они могут дать отпор чудовищам," +
                    $"\nкоторые в их мире совсем не редкость." +
                    $"\n" +
                    $"\nБез пощады уничтожайте врагов," +
                    $"\nиграя за профессионального охотника на чудовищ.В вашем распоряжении целый арсенал улучшаемого оружия," +
                    $"\nмутагенных зелий и боевых заклинаний." +
                    $"\nОхотьтесь на самых разных чудовищ," +
                    $"\nот диких высокогорных тварей до хитрых созданий," +
                    $"\nчто таятся в тёмных переулках больших городов." +
                    $"\nНа заработанные деньги улучшайте оружие," +
                    $"\nприобретайте небывалую броню... или просто спускайте всё на скачки," +
                    $"\nигры," +
                    $"кулачные бои и другие приятные развлечения."
                };
                context.Games.Add(game1);
                context.Pictures.AddRange(
                    new Picture { GameId = game1.Id, Value = "https://steamcdn-a.akamaihd.net/steam/apps/292030/ss_107600c1337accc09104f7a8aa7f275f23cad096.1920x1080.jpg" },
                    new Picture { GameId = game1.Id, Value = "https://steamcdn-a.akamaihd.net/steam/apps/292030/ss_64eb760f9a2b67f6731a71cce3a8fb684b9af267.1920x1080.jpg" },
                    new Picture { GameId = game1.Id, Value = "https://steamcdn-a.akamaihd.net/steam/apps/292030/ss_b74d60ee215337d765e4d20c8ca6710ae2362cc2.1920x1080.jpg" },
                    new Picture { GameId = game1.Id, Value = "https://steamcdn-a.akamaihd.net/steam/apps/292030/ss_90a33d7764a2d23306091bfcb52265c3506b4fdb.1920x1080.jpg" }
                );

                Game game2 = new Game
                {
                    MainImage = "https://en.wikipedia.org/wiki/Fallout_(series)#/media/File:Fallout_logo.jpg",
                    Name = "Fallout",
                    Developer = "Bethesda",
                    Price = 23192.91,
                    ReleaseDate = new DateTime(2019, 11, 5),
                    Rating = 9.2,
                    Genre = "Action-adventure",
                    Description = "Postapocalipse game, you must survive in that doomed world"
                };
                context.Games.Add(game2);
                context.SaveChanges();
            }
        }

        private void ListBoxGameClicked(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                using (Context context = new Context("Server=BorisHome\\Boris;Database=GameAd;Trusted_Connection=True;"))
                {
                    Game chosenGame = context.Games.Where(g => g.Id == Guid.Parse(item.Content.ToString())).FirstOrDefault<Game>();
                    GameWin gameWindow = new GameWin();
                    gameWindow.Title = chosenGame.Name;
                    gameWindow.Icon = new BitmapImage(new Uri(chosenGame.MainImage, UriKind.Absolute));
                    gameWindow.name.Text = chosenGame.Name;
                    gameWindow.mainImage.Source = new BitmapImage(new Uri(chosenGame.MainImage, UriKind.Absolute));
                    gameWindow.developer.Text = "Developer: " + chosenGame.Developer;
                    gameWindow.genre.Text = "Genre: " + chosenGame.Genre;
                    gameWindow.description.Text = "Description: " + chosenGame.Description;
                    gameWindow.price.Text = "Price: " + chosenGame.Price.ToString() + " тенге";
                    gameWindow.rating.Text = "Rating: " + chosenGame.Rating.ToString() + "/10";
                    gameWindow.releaseDate.Text = "Release Date: " + chosenGame.ReleaseDate.Date.ToString("D");
                    gameWindow.imagesOfGame.ItemsSource = context.Pictures.Where(p => p.GameId == chosenGame.Id).ToList();
                    gameWindow.ShowDialog();
                }
            }
        }
    }
}
