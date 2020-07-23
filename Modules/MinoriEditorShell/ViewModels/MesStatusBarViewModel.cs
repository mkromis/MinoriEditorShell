using MinoriEditorShell.Services;
using MvvmCross.ViewModels;
using System;
using System.Drawing;

namespace MinoriEditorShell.ViewModels
{
    /// <summary>
    /// Class Wider Status bar
    /// </summary>
    public class MesStatusBarViewModel : MvxViewModel, IMesStatusBar
    {
        #region Fields

        /// <summary>
        /// The line number
        /// </summary>
        private Int32? _lineNumber;

        /// <summary>
        /// The insert mode
        /// </summary>
        private Boolean? _insertMode;

        /// <summary>
        /// The col position
        /// </summary>
        private Int32? _colPosition;

        /// <summary>
        /// The char position
        /// </summary>
        private Int32? _charPosition;

        /// <summary>
        /// The p max
        /// </summary>
        private UInt32 _pMax;

        /// <summary>
        /// The _p val
        /// </summary>
        private UInt32 _pVal;

        /// <summary>
        /// The _foreground
        /// </summary>
        private Color _foreground;

        /// <summary>
        /// The _background
        /// </summary>
        private Color _background;

        /// <summary>
        /// The _show progress
        /// </summary>
        private Boolean _showProgress;

        /// <summary>
        /// The _anim image
        /// </summary>
        private Image _animImage;

        /// <summary>
        /// The _is frozen
        /// </summary>
        private Boolean _isFrozen;

        /// <summary>
        /// The _text
        /// </summary>
        private String _text;

        #endregion Fields

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="IMesStatusbarService"/> class.
        /// </summary>
        public MesStatusBarViewModel() => Clear();

        #endregion CTOR

        #region IStatusbarService members

        /// <summary>
        /// Animations the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public Boolean Animation(Image image)
        {
            AnimationImage = image;
            return true;
        }

        /// <summary>
        /// Clears this status bar.
        /// </summary>
        /// <returns><c>true</c> if successfully, <c>false</c> otherwise</returns>
        public Boolean Clear()
        {
            Foreground = Color.White;
            Background = Color.FromArgb(0, 122, 204);
            Text = "Ready";
            IsFrozen = false;
            ShowProgressBar = false;
            InsertMode = null;
            LineNumber = null;
            CharPosition = null;
            ColPosition = null;
            AnimationImage = null;
            return true;
        }

        /// <summary>
        /// Freezes the output.
        /// </summary>
        /// <returns><c>true</c> if frozen, <c>false</c> otherwise</returns>
        public Boolean FreezeOutput() => IsFrozen;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is frozen.
        /// </summary>
        /// <value><c>true</c> if this instance is frozen; otherwise, <c>false</c>.</value>
        public Boolean IsFrozen
        {
            get => _isFrozen;
            set => SetProperty(ref _isFrozen, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public String Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        /// <summary>
        /// Gets or sets the foreground.
        /// </summary>
        /// <value>The foreground.</value>
        public Color Foreground
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Color Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [insert mode].
        /// </summary>
        /// <value><c>null</c> if [insert mode] contains no value, <c>true</c> if [insert mode]; otherwise, <c>false</c>.</value>
        public Boolean? InsertMode
        {
            get => _insertMode;
            set => SetProperty(ref _insertMode, value);
        }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public Int32? LineNumber
        {
            get => _lineNumber;
            set => SetProperty(ref _lineNumber, value);
        }

        /// <summary>
        /// Gets or sets the char position.
        /// </summary>
        /// <value>The char position.</value>
        public Int32? CharPosition
        {
            get => _charPosition;
            set => SetProperty(ref _charPosition, value);
        }

        /// <summary>
        /// Gets or sets the col position.
        /// </summary>
        /// <value>The col position.</value>
        public Int32? ColPosition
        {
            get => _colPosition;
            set => SetProperty(ref _colPosition, value);
        }

        /// <summary>
        /// Progresses the specified on.
        /// </summary>
        /// <param name="On">if set to <c>true</c> [on].</param>
        /// <param name="current">The current.</param>
        /// <param name="total">The total.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public Boolean Progress(Boolean On, UInt32 current, UInt32 total)
        {
            ShowProgressBar = On;
            ProgressMaximum = total;
            ProgressValue = current;
            return true;
        }

        /// <summary>
        /// Gets or sets the progress maximum.
        /// </summary>
        /// <value>The progress maximum.</value>
        public UInt32 ProgressMaximum
        {
            get => _pMax;
            set => SetProperty(ref _pMax, value);
        }

        /// <summary>
        /// Gets or sets the progress value.
        /// </summary>
        /// <value>The progress value.</value>
        public UInt32 ProgressValue
        {
            get => _pVal;
            set => SetProperty(ref _pVal, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show progress bar].
        /// </summary>
        /// <value><c>true</c> if [show progress bar]; otherwise, <c>false</c>.</value>
        public Boolean ShowProgressBar
        {
            get => _showProgress;
            set => SetProperty(ref _showProgress, value);
        }

        /// <summary>
        /// Gets or sets the animation image.
        /// </summary>
        /// <value>The animation image.</value>
        public Image AnimationImage
        {
            get => _animImage;
            set => SetProperty(ref _animImage, value);
        }

        #endregion IStatusbarService members
    }
}