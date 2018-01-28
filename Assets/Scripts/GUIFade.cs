using UnityEngine;
using System.Collections;
using UnityEngine.Events;


/// <summary>
/// Component used to fade objects under a canvas in and out.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class GUIFade : MonoBehaviour
{

    /// <summary>
    /// The curve to fade in by. Default is ease-in, ease-out.
    /// </summary>
    [Tooltip("The curve to fade in by. Default is ease-in, ease-out.")]
    public AnimationCurve FadeInCurve;

    /// <summary>
    /// The curve to fade in by. Default is ease-in, ease-out.
    /// </summary>
    [Tooltip("The curve to fade out by. Default is ease-in, ease-out.")]
    public AnimationCurve FadeOutCurve;

    /// <summary>
    /// How long it takes to fade an object in or out.
    /// </summary>
    [Tooltip("How long it takes to fade an object in or out.")]
    public float FadeTime = 0.65f;

    CanvasGroup cg;

    public UnityEvent onFadeInComplete;
    public UnityEvent onFadeOutComplete;

    /// <summary>
    /// Current fade state.
    /// </summary>
    public enum Fade
    {
        /// <summary>
        /// Fade in the game object 
        /// </summary>
        In,
        /// <summary>
        /// Fade out the game object 
        /// </summary>
        Out,
        /// <summary>
        /// Start with the object disabled.
        /// </summary>
        Off,
        /// <summary>
        /// Start with the object enabled.
        /// </summary>
        On
    }
    [Tooltip("Current fade state. In: Fade in the game object. Out: Fade out the game object. Off: Start with the object disabled. On: Start with the object enabled.")]
    [SerializeField]
    private Fade fadeState = Fade.On;

    /// <summary>
    /// Current fade state. In: Fade in the game object. Out: Fade out the game object. Off: Start with the object disabled. On: Start with the object enabled.
    /// </summary>
    public Fade FadeState
    {
        get { return fadeState; }
        set
        {
            fadeState = value;
            if (value == Fade.Off)
            {
                gameObject.SetActive(false);
                cg.alpha = 0;
            }
            else
            {
                gameObject.SetActive(true);
            }
            if (value == Fade.On)
            {
                cg.alpha = 1;
            }
        }
    }

    /// <summary>
    /// OnValidate is used to run code in the Unity editor whenever the value of a field of this component has changed.
    /// </summary>
    void OnValidate()
    {
        cg = GetComponent<CanvasGroup>();
        FadeState = fadeState;
        if (fadeState == Fade.In)
        {
            cg.alpha = 0;
        }
        else if (fadeState == Fade.Out)
        {
            cg.alpha = 1;
        }
    }

    /// <summary>
    /// Reset is called when the user hits the Reset button in the Inspector's context menu or when adding the component the first time.
    /// This function is only called in editor mode. Reset is most commonly used to give good default values in the inspector.
    /// </summary>
    void Reset()
    {
        createCurves();
        OnValidate();
    }

    /// <summary>
    /// If fading in or out, set the initial fade state for a transition and then do the transition.
    /// Otherwise, set the GameObject visible or invisible (on or off).
    /// </summary>
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        if (cg == null)
            cg = gameObject.AddComponent<CanvasGroup>();

        createCurves();
        switch (FadeState)
        {
            case Fade.In:
                FadeState = Fade.Off;
                FadeIn();
                break;

            case Fade.Out:
                FadeState = Fade.On;
                FadeOut();
                break;

            case Fade.Off:
                cg.alpha = 0;
                gameObject.SetActive(false);
                break;

            case Fade.On:
                cg.alpha = 1;
                gameObject.SetActive(true);
                break;
        }
    }
    /// <summary>
    /// A basic event type for fade events.
    /// </summary>
    public delegate void Faded();

    /// <summary>
    /// This event fires as the fade in finishes.
    /// </summary>
    public event Faded FadeInComplete;

    /// <summary>
    /// This event fires as the fade out finishes.
    /// </summary>
    public event Faded FadeOutComplete;

    /// <summary>
    /// This event fires as the fade in begins.
    /// </summary>
    public event Faded FadeInStart;

    /// <summary>
    /// This event fires as the fade out begins.
    /// </summary>
    public event Faded FadeOutStart;

    /// <summary>
    /// Coroutine that gradually fades into the desired state.
    /// </summary>
    /// <param name="fadeCurve">Curve to follow while fadeing. Default is ease-in, ease-out.</param>
    /// <param name="fadeType">Fade type to use. FadeState.In or FadeState.Out</param>
    /// <param name="fadeTime">Length of fade. Leave blank to use the component values from the Unity Inspector window.</param>
    /// <returns></returns>
    IEnumerator doFade(AnimationCurve fadeCurve, Fade fadeType, float fadeTime = -1)
    {
        yield return null;

        if (fadeTime < 0)
            fadeTime = FadeTime;
        float startTime = Time.unscaledTime;
        float time = 0;

        // Adjust time passed to account for current fade in or out (when switching back and forth rapidly).
        // We want to start at the same fade level as the fade we just interrupted.
        if (FadeState == Fade.In)
        {
            startTime -= fadeTime * cg.alpha;
        }
        else
        {
            startTime -= fadeTime * (1 - cg.alpha);
        }

        // Do the actual fade here.
        while (time < fadeTime && FadeState == fadeType)
        {
            time = Time.unscaledTime - startTime;
            cg.alpha = fadeCurve.Evaluate(time / fadeTime);
            yield return null;
        }
        if (FadeState == fadeType)
        {
            // Set final fade state (Fade.On or Fade.Off) and send completion event.
            if (fadeType == Fade.In)
            {
                FadeState = Fade.On;

                //onFadeInComplete.Invoke();
                if (FadeInComplete != null)
                    FadeInComplete();
            }
            else
            {
                FadeState = Fade.Off;
                //onFadeOutComplete.Invoke();

                if (FadeOutComplete != null)
                    FadeOutComplete();
            }
        }
    }


    /// <summary>
    /// The current fade. Any new fade first stops the current fade.
    /// </summary>
    IEnumerator fade;

    /// <summary>
    /// Fade in this game object over fadeTime seconds.
    /// </summary>
    /// <param name="fadeTime">Length of fade-in. Leave blank to use the component values from the Unity Inspector window.</param>
    public void FadeIn(float fadeTime = -1)
    {
        FadeState = Fade.In;
        if (FadeInStart != null) {
            FadeInStart();
        }
        if (fade != null)
            StopCoroutine(fade);
        fade = doFade(FadeInCurve, Fade.In, fadeTime);
        StartCoroutine(fade);
    }

    /// <summary>
    /// Fade out this game object over fadeTime seconds.
    /// </summary>
    /// <param name="fadeTime">Length of fade-out. Leave blank to use the component values from the Unity Inspector window.</param>
    public void FadeOut(float fadeTime = -1)
    {
        FadeState = Fade.Out;
        if (FadeOutStart != null) {
            FadeOutStart();
        }
        if (fade != null)
            StopCoroutine(fade);
        fade = doFade(FadeOutCurve, Fade.Out, fadeTime);
        StartCoroutine(fade);
    }

    /// <summary>
    /// Fades out game object. GameObject MUST BE UNDER A CANVAS in the hierarchy view of the level.
    /// </summary>
    /// <param name="fade">GameObject to fade out.</param>
    /// <param name="time">Time to fade in. Leave blank to use the component values from the Unity Inspector window.</param>
    public static void FadeOut(GameObject fade, float time = -1)
    {
        GUIFade tofade = createFade(fade);
        tofade.FadeOut(time);
    }

    /// <summary>
    /// Fades in game object. GameObject MUST BE UNDER A CANVAS in the hierarchy view of the level.
    /// </summary>
    /// <param name="fade">GameObject to fade in.</param>
    /// <param name="time">Time to fade in. Leave blank to use the component values from the Unity Inspector window.</param>
    public static void FadeIn(GameObject fade, float time = -1)
    {
        GUIFade toFade = createFade(fade);
        toFade.FadeIn(time);
    }

    /// <summary>
    /// Fades out one game object and fades in another. GameObjects MUST BE UNDER A CANVAS in the hierarchy view of the level.
    /// </summary>
    /// <param name="fadeOut">GameObject to fade out.</param>
    /// <param name="fadeIn">GameObject to fade in.</param>
    /// <param name="fadeTime">Time to fade in. Leave blank to use the component values from the Unity Inspector window.</param>
    public static void CrossFade(GameObject fadeOut, GameObject fadeIn, float fadeTime = -1)
    {
        GUIFade toFadeIn = createFade(fadeIn);
        toFadeIn.FadeIn(fadeTime);
        
        GUIFade toFadeOut = createFade(fadeOut);
        toFadeOut.FadeOut(fadeTime);
    }

    /// <summary>
    /// Create the fade-in and fade-out curves if they don't exist yet or got erased.
    /// </summary>
    void createCurves()
    {
        if (FadeInCurve == null || FadeInCurve.keys.Length == 0)
        {
            Keyframe[] frames = new Keyframe[2];
            frames[0] = new Keyframe(0, 0, 0, 0);
            frames[1] = new Keyframe(1, 1, 0, 0);
            FadeInCurve = new AnimationCurve(frames);
        }
        if (FadeOutCurve == null || FadeOutCurve.keys.Length == 0)
        {
            Keyframe[] frames = new Keyframe[2];
            frames[0] = new Keyframe(0, 1, 0, 0);
            frames[1] = new Keyframe(1, 0, 0, 0);
            FadeOutCurve = new AnimationCurve(frames);
        }
    }

    /// <summary>
    /// Check if GUIFade component already exists for this game object. If not, create one.
    /// </summary>
    /// <param name="newFade"></param>
    /// <returns></returns>
    static GUIFade createFade(GameObject newFade)
    {
        GUIFade fade = newFade.GetComponent<GUIFade>();
        if (!fade)
            fade = newFade.AddComponent<GUIFade>();
        fade.createCurves();

        return fade;
    }
}


/// <summary>
/// An extension class is used to add methods to existing classes.
/// In this case, we add FadeIn, FadeOut, and CrossFade options to GameObjects.
/// </summary>
public static class GUIFadeExtensions
{
    /// <summary>
    /// Fades in game object. GameObject MUST BE UNDER A CANVAS in the hierarchy view of the level.
    /// </summary>
    /// <param name="fade">GameObject to fade in.</param>
    /// <param name="time">Time to fade in. Leave blank to use the component values from the Unity Inspector window.</param>
    public static void FadeIn(this GameObject fade, float time = -1)
    {
        GUIFade.FadeIn(fade, time);
    }

    /// <summary>
    /// Fades out game object. GameObject MUST BE UNDER A CANVAS in the hierarchy view of the level.
    /// </summary>
    /// <param name="fade">GameObject to fade out.</param>
    /// <param name="time">Time to fade in. Leave blank to use the component values from the Unity Inspector window.</param>
    public static void FadeOut(this GameObject fade, float time = -1)
    {
        GUIFade.FadeOut(fade, time);
    }
    /// <summary>
    /// Fades out one game object and fades in another. GameObjects MUST BE UNDER A CANVAS in the hierarchy view of the level.
    /// </summary>
    /// <param name="fadeOut">GameObject to fade out.</param>
    /// <param name="fadeIn">GameObject to fade in.</param>
    /// <param name="time">Time to fade in. Leave blank to use the component values from the Unity Inspector window.</param>
    public static void CrossFade(this GameObject fadeOut, GameObject fadeIn, float time = -1)
    {
        GUIFade.CrossFade(fadeOut, fadeIn, time);
    }
}
