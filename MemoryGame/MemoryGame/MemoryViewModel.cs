namespace MemoryClassLibrary {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.ApplicationModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// If you don't, or can't, derive from PropertyChangedNotifier, you can still
    /// use the PropertyChangedNotifierHelper class.
    /// </summary>
    public static class PropertyChangedNotifierHelper {
        #region methods
        public static bool Assign<T>(ref T target, T value) {
            if (!Object.Equals(target, value)) {
                target = value;
                return true;
            }
            return false;
        }

        public static bool AssignAndRaisePropertyChanged<T>(INotifyPropertyChanged thisReference, PropertyChangedEventHandler propertyChanged, string propertyName, ref T target, T value) {
            if (!Object.Equals(target, value)) {
                target = value;
                PropertyChangedNotifierHelper.RaisePropertyChanged<T>(thisReference, propertyChanged, propertyName);
                return true;
            }
            return false;
        }

        public static void RaisePropertyChanged<T>(INotifyPropertyChanged thisReference, PropertyChangedEventHandler propertyChanged, string propertyName) {
            if (propertyChanged != null) {
                propertyChanged(thisReference, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion methods
    }

    public abstract class PropertyChangedNotifier : INotifyPropertyChanged {
        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion INotifyPropertyChanged members

        #region methods
        protected bool AssignAndRaisePropertyChanged<T>(string propertyName, ref T target, T value) {
            return PropertyChangedNotifierHelper.AssignAndRaisePropertyChanged<T>(this, this.PropertyChanged, propertyName, ref target, value);
        }

        protected void RaisePropertyChanged<T>(string propertyName) {
            PropertyChangedNotifierHelper.RaisePropertyChanged<T>(this, this.PropertyChanged, propertyName);
        }
        #endregion methods
    }

    public class Timer : PropertyChangedNotifier {
        #region fields
        private bool isRunning;
        private DateTime lastTick;
        private TimeSpan time;
        private DispatcherTimer timer;
        #endregion fields

        #region properties
        public string FormattedTime {
            get {
                return string.Format("{0:#0}:{1:00}:{2:00}", this.time.Hours, this.time.Minutes, this.time.Seconds);
            }
        }
        public bool IsRunning {
            get {
                return this.isRunning;
            }
            set {
                if (this.isRunning != value) {
                    this.isRunning = value;
                    if (this.isRunning) {
                        this.StartTimer();
                    }
                    else {
                        this.StopTimer();
                    }
                    this.RaisePropertyChanged<bool>("IsRunning");
                }
            }
        }

        #endregion properties

        #region methods
        public bool Increment {
            get;
            set;
        }
        public void Reset() {
            this.time = new TimeSpan();
        }
        private void StartTimer() {
            if (this.timer != null) {
                this.StopTimer();
            }
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(1);
            this.timer.Tick += new EventHandler<object>(timer_Tick);
            this.lastTick = DateTime.Now;
            this.timer.Start();
        }
        private void StopTimer() {
            if (this.timer != null) {
                this.timer.Stop();
                this.timer = null;
            }
        }
        #endregion methods

        #region eventhandlers
        void timer_Tick(object sender, object e) {
            DateTime now = DateTime.Now;
            TimeSpan diff = now - this.lastTick;
            this.lastTick = now;

            if (this.Increment) {
                this.time = this.time.Add(diff);
            }
            else {
                this.time = this.time.Subtract(diff);
            }

            if (this.time.TotalMilliseconds <= 0) {
                this.time = TimeSpan.FromMilliseconds(0);
                this.IsRunning = false;
            }
            this.RaisePropertyChanged<string>("FormattedTime");
        }
        #endregion eventhandlers
    }

    /// RelayCommand is a very easy-to-use implementation of ICommand.
    /// You can use a RelayCommand to expose viewmodel functionality as an ICommand, as well as
    /// supply the condition that determines its availability.
    /// A control in the view can use a Binding to both invoke this command and update in response to its availability.
    public sealed class RelayCommand : ICommand {
        #region fields
        readonly Predicate<object> canExecute;
        readonly Action<object> execute;
        #endregion fields

        #region constructors
        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion constructors

        #region methods
#if SILVERLIGHT || NETFX_CORE
        internal void RaiseCanExecuteChanged() {
            if (this.CanExecuteChanged != null) {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
#endif
        #endregion methods

        #region ICommand events
        public event EventHandler CanExecuteChanged
#if SILVERLIGHT || NETFX_CORE
;
#else
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
#endif
        #endregion ICommand events

        #region ICommand methods
        public bool CanExecute(object parameter) {
            return this.canExecute != null ? this.canExecute(parameter) : true;
        }

        public void Execute(object parameter) {
            if (this.execute != null) {
                this.execute(parameter);
            }
        }
        #endregion ICommand methods
    }

    public class MemoryViewModel : PropertyChangedNotifier {
        #region fields
        private List<Card> allCards;
        public List<BoardSize> BoardSizes { get; private set; }
        private int tries;
        private List<Card> cardsOnBoard;
        private static Card designTimeCard;
        private bool isPaused;
        private int cardsMatched;
        public List<PictureSet> PictureSets { get; private set; }
        private int picturesInASet = 8;
        private BoardSize selectedBoardSize;
        private Card selectedCard;
        private PictureSet selectedPictureSet;
        private RelayCommand togglePauseResumeCommand;
        #endregion fields

        #region properties
        public int Tries {
            get {
                return tries;
            }
            set {
                this.AssignAndRaisePropertyChanged<int>("Tries", ref this.tries, value);
            }
        }

        public List<Card> CardsOnBoard {
            get {
                return cardsOnBoard;
            }
            set {
                this.AssignAndRaisePropertyChanged<List<Card>>("CardsOnBoard", ref this.cardsOnBoard, value);
            }
        }

        public int CardsMatched {
            get {
                return cardsMatched;
            }
            set {
                this.AssignAndRaisePropertyChanged<int>("CardsMatched", ref this.cardsMatched, value);
            }
        }

        internal static Card DesignTimeCard {
            get {
                return MemoryViewModel.designTimeCard;
            }
        }

        private bool IsPaused {
            get {
                return this.isPaused;
            }
            set {
                if (this.isPaused != value) {
                    this.isPaused = value;
                    this.Timer.IsRunning = !this.isPaused;
                    if (DesignMode.DesignModeEnabled) {
                        this.Timer.IsRunning = false;
                    }
                    this.RaisePropertyChanged<string>("PauseResumeButtonContent");
                    this.RaisePropertyChanged<Visibility>("PausedVisibility");
                }
            }
        }

        public string PauseResumeButtonContent {
            get {
                return (this.IsPaused) ? "Start timer" : "Stop timer";
            }
        }

        public Visibility PausedVisibility {
            get {
                return (this.IsPaused) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public BoardSize SelectedBoardSize {
            get {
                return this.selectedBoardSize;
            }
            set {
                this.AssignAndRaisePropertyChanged<BoardSize>("SelectedBoardSize", ref this.selectedBoardSize, value);
                this.OnSelectedBoardSizeOrPictureSetChanged();
                this.RaisePropertyChanged<int>("SelectedBoardSizeAsInt");
            }
        }

        public int SelectedBoardSizeAsInt {
            get {
                return (int)this.SelectedBoardSize.BoardSizeEnum;
            }
        }

        public Card SelectedCard {
            get {
                return selectedCard;
            }
            set {
                if (this.selectedCard != value && (value == null || value.CardState != CardStatesEnum.Matched)) {
                    Card previouslySelectedCard = this.selectedCard;
                    this.selectedCard = value;

                    if (previouslySelectedCard != null) {
                        if (this.SelectedCard != null && this.SelectedCard.ImageUri == previouslySelectedCard.ImageUri) {
                            previouslySelectedCard.CardState = CardStatesEnum.Matched;
                            this.SelectedCard.CardState = CardStatesEnum.Matched;
                            this.CardsMatched += 2;
                        }
                        else {
                            previouslySelectedCard.CardState = CardStatesEnum.FaceDown;
                            if (this.SelectedCard != null) {
                                this.SelectedCard.CardState = CardStatesEnum.FaceUp;
                                this.SelectedCard.CardState = CardStatesEnum.FaceDown;
                            }
                        }
                        this.selectedCard = null;
                        ++this.Tries;
                    }
                    else if (this.SelectedCard != null) {
                        if (this.SelectedCard.CardState == CardStatesEnum.FaceDown) {
                            this.SelectedCard.CardState = CardStatesEnum.FaceUp;
                        }
                    }

                    this.RaisePropertyChanged<Card>("SelectedCard");
                }
            }
        }

        public PictureSet SelectedPictureSet {
            get {
                return selectedPictureSet;
            }
            set {
                this.AssignAndRaisePropertyChanged<PictureSet>("SelectedPictureSet", ref this.selectedPictureSet, value);
                this.OnSelectedBoardSizeOrPictureSetChanged();
            }
        }

        public Timer Timer { get; set; }
        #endregion properties

        #region constructors
        static MemoryViewModel() {
            MemoryViewModel.designTimeCard = new Card("Assets/Argentina/03.jpg", PictureSetEnum.Argentina);
        }

        public MemoryViewModel() {
            this.Timer = new Timer();
            this.Timer.Increment = true;

            this.IsPaused = true;
            //this.IsPaused = false;

            this.PictureSets = new List<PictureSet>();
            this.PictureSets.Add(new PictureSet() { PictureSetEnum = PictureSetEnum.Argentina, Thumbnail = "Assets/Argentina/03.jpg", Name = "Argentina" });
            this.PictureSets.Add(new PictureSet() { PictureSetEnum = PictureSetEnum.China, Thumbnail = "Assets/China/01.jpg", Name = "China" });
            this.PictureSets.Add(new PictureSet() { PictureSetEnum = PictureSetEnum.Default, Thumbnail = "Assets/Default/02.jpg", Name = "Default" });
            this.PictureSets.Add(new PictureSet() { PictureSetEnum = PictureSetEnum.India, Thumbnail = "Assets/India/01.jpg", Name = "India" });
            this.PictureSets.Add(new PictureSet() { PictureSetEnum = PictureSetEnum.Mexico, Thumbnail = "Assets/Mexico/01.jpg", Name = "Mexico" });
            this.PictureSets.Add(new PictureSet() { PictureSetEnum = PictureSetEnum.Zoo, Thumbnail = "Assets/Zoo/01.jpg", Name = "Zoo" });
            this.SelectedPictureSet = this.PictureSets[0];

            this.allCards = new List<Card>();
            //if (!DesignMode.DesignModeEnabled)
            {
                this.allCards.Add(new Card("Assets/Argentina/03.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/04.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/05.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/06.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/07.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/08.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/09.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/10.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/11.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/12.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/13.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/14.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/15.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/16.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/17.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/18.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/19.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/20.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/21.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/22.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/23.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/24.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/25.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/26.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/27.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/28.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/29.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/30.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/31.jpg", PictureSetEnum.Argentina));
                this.allCards.Add(new Card("Assets/Argentina/32.jpg", PictureSetEnum.Argentina));

                this.allCards.Add(new Card("Assets/China/01.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/02.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/03.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/04.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/05.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/06.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/07.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/08.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/09.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/10.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/11.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/12.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/13.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/14.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/15.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/16.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/17.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/18.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/19.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/20.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/21.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/22.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/23.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/24.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/25.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/26.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/27.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/28.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/29.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/30.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/31.jpg", PictureSetEnum.China));
                this.allCards.Add(new Card("Assets/China/32.jpg", PictureSetEnum.China));

                this.allCards.Add(new Card("Assets/Default/01.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/02.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/03.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/04.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/05.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/06.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/07.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/08.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/09.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/10.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/11.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/12.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/13.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/14.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/15.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/16.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/17.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/18.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/19.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/20.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/21.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/22.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/23.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/24.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/25.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/26.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/27.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/28.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/29.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/30.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/31.jpg", PictureSetEnum.Default));
                this.allCards.Add(new Card("Assets/Default/32.jpg", PictureSetEnum.Default));

                this.allCards.Add(new Card("Assets/India/01.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/02.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/03.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/04.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/05.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/06.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/07.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/08.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/09.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/10.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/11.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/12.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/13.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/14.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/15.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/16.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/17.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/18.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/19.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/20.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/21.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/22.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/23.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/24.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/25.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/26.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/27.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/28.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/29.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/30.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/31.jpg", PictureSetEnum.India));
                this.allCards.Add(new Card("Assets/India/32.jpg", PictureSetEnum.India));

                this.allCards.Add(new Card("Assets/Mexico/01.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/02.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/03.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/04.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/05.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/06.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/07.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/08.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/09.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/10.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/11.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/12.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/13.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/14.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/15.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/16.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/17.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/18.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/19.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/20.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/21.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/22.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/23.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/24.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/25.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/26.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/27.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/28.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/29.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/30.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/31.jpg", PictureSetEnum.Mexico));
                this.allCards.Add(new Card("Assets/Mexico/32.jpg", PictureSetEnum.Mexico));

                this.allCards.Add(new Card("Assets/Zoo/01.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/02.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/03.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/04.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/05.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/06.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/07.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/08.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/09.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/10.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/11.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/12.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/13.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/14.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/15.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/16.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/17.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/18.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/19.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/20.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/21.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/22.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/23.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/24.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/25.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/26.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/27.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/28.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/29.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/30.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/31.jpg", PictureSetEnum.Zoo));
                this.allCards.Add(new Card("Assets/Zoo/32.jpg", PictureSetEnum.Zoo));

            }

            this.BoardSizes = new List<BoardSize>();
            this.BoardSizes.Add(new BoardSize() { BoardSizeEnum = BoardSizeEnum.Small });
            this.BoardSizes.Add(new BoardSize() { BoardSizeEnum = BoardSizeEnum.Medium });
            this.BoardSizes.Add(new BoardSize() { BoardSizeEnum = BoardSizeEnum.Large });
            this.SelectedBoardSize = this.BoardSizes[0];
        }
        #endregion constructors

        #region methods
        private void OnSelectedBoardSizeOrPictureSetChanged() {
            if (this.SelectedBoardSize != null && this.SelectedPictureSet != null) {
                this.CardsOnBoard = Shuffle(((from p in this.allCards where p.PictureSetEnum == this.SelectedPictureSet.PictureSetEnum select p).Take(this.picturesInASet)).ToArray());
                this.ResetScore();
                foreach (Card card in this.allCards) {
                    card.CardState = CardStatesEnum.FaceDown;
                }
            }
        }

        private void ResetScore() {
            Timer.Reset();
            this.CardsMatched = 0;
            this.Tries = 0;
        }

        private List<Card> Shuffle(Card[] cardArray) {
            var temp = new Card[(Int32)Math.Pow((Int32)this.SelectedBoardSize.BoardSizeEnum, 2)];

            int cardArrayIndex = 0;

            for (int i = 0; i < temp.Length; i += 2) {
                if (cardArrayIndex >= cardArray.Length) {
                    cardArrayIndex = 0;
                }
                temp[i] = cardArray[cardArrayIndex].Clone();
                temp[i + 1] = cardArray[cardArrayIndex].Clone();
                ++cardArrayIndex;
            }

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = temp.Length - 1; i > 0; i--) {
                int r = rnd.Next(0, i);
                var tmp = temp[i];
                temp[i] = temp[r];
                temp[r] = tmp;
            }

            return temp.ToList();
        }

        private void TogglePauseResume() {
            this.IsPaused = !this.IsPaused;
        }
        #endregion methods

        #region commands
        public RelayCommand TogglePauseResumeCommand {
            get {
                if (this.togglePauseResumeCommand == null) {
                    this.togglePauseResumeCommand = new RelayCommand(
                        param => this.TogglePauseResume());
                }
                return this.togglePauseResumeCommand;
            }
        }
        #endregion commands
    }

    public enum CardStatesEnum {
        FaceDown = 0,
        FaceUp = 1,
        Matched = 2
    }

    public interface IControlVM {
        #region properties
        // Only necessary for use with VisualStateManager.GoToState(), and the viewmodel should only use it for that purpose to adhere to separated presentation.
        UserControl View { get; set; }
        #endregion  properties
    }

    public class Card : PropertyChangedNotifier, IControlVM {
        #region fields
        private CardStatesEnum cardState = CardStatesEnum.FaceDown;
        private UserControl view = null;
        #endregion fields

        #region properties
        internal CardStatesEnum CardState {
            get {
                return this.cardState;
            }
            set {
                if (this.cardState != value) {
                    this.AssignAndRaisePropertyChanged<CardStatesEnum>("CardState", ref this.cardState, value);
                    this.RaisePropertyChanged<Visibility>("ImageVisibility");
                    this.RaisePropertyChanged<Visibility>("FoundVisibility");
                    this.RaisePropertyChanged<Visibility>("OpenVisibility");
                    this.UpdateStates(true);
                }
            }
        }

        public Visibility FoundVisibility {
            get {
                return (this.CardState == CardStatesEnum.Matched) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public string ImageUri { get; set; }

        public Visibility ImageVisibility {
            get {
                return (this.CardState == CardStatesEnum.FaceDown) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility OpenVisibility {
            get {
                return (this.CardState == CardStatesEnum.FaceUp) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public PictureSetEnum PictureSetEnum { get; set; }

        public UserControl View {
            get { return this.view; }
            set {
                if (this.view != value) {
                    this.view = value;
                    this.UpdateStates(false);
                }
            }
        }
        #endregion properties

        #region constructors
        public Card()
            : this(MemoryViewModel.DesignTimeCard.ImageUri, MemoryViewModel.DesignTimeCard.PictureSetEnum) {
        }

        public Card(string imageUri, PictureSetEnum pictureSetEnum) {
            this.ImageUri = imageUri;
            this.PictureSetEnum = pictureSetEnum;
            this.CardState = CardStatesEnum.FaceDown;
        }
        #endregion constructors

        #region methods
        internal Card Clone() {
            return new Card(this.ImageUri, this.PictureSetEnum);
        }

        private void InternalGoToState(string stateName, bool useTransitions) {
            if (this.View != null) {
                VisualStateManager.GoToState(View, stateName, useTransitions);
            }
        }

        private void UpdateStates(bool useTransitions) {
            if (this.CardState == CardStatesEnum.FaceDown) {
                this.InternalGoToState(CardStatesEnum.FaceDown.ToString(), useTransitions);
            }
            else if (this.CardState == CardStatesEnum.FaceUp) {
                this.InternalGoToState(CardStatesEnum.FaceUp.ToString(), useTransitions);
            }
            else {
                this.InternalGoToState(CardStatesEnum.Matched.ToString(), useTransitions);
            }
        }
        #endregion methods
    }

    public class BoardSize {
        public BoardSizeEnum BoardSizeEnum { get; set; }
        public string Name {
            get { return string.Format("{0}x{0}", (int)this.BoardSizeEnum); }
        }
    }

    public enum BoardSizeEnum {
        Small = 4,
        Medium = 6,
        Large = 8
    }

    public enum PictureSetEnum {
        Default,
        Argentina,
        China,
        India,
        Mexico,
        Zoo
    }

    public class PictureSet {
        public string Name { get; set; }
        public PictureSetEnum PictureSetEnum { get; set; }
        public string Thumbnail { get; set; }
    }

    public class ViewBase : UserControl {
        private IControlVM ControlVM {
            get {
                return this.DataContext as IControlVM;
            }
        }

        public ViewBase() {
            this.Loaded += new RoutedEventHandler(ViewBase_Loaded);
        }

        void ViewBase_Loaded(object sender, RoutedEventArgs e) {
#if SILVERLIGHT || NETFX_CORE
            this.UseLayoutRounding = false;
#endif
            // Only necessary for use with VisualStateManager.GoToState(), and the viewmodel should only use it for that purpose to adhere to separated presentation.
            if (this.ControlVM != null) {
                this.ControlVM.View = this;
            }
        }
    }
}