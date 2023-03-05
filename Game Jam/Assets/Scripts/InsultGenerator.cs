using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsultGenerator : MonoBehaviour
{
    [SerializeField] Text insultText;

    void Start()
    {
        GenerateInsult();
    }

    void GenerateInsult()
    {
        string[] insults = new string[] {
    "Have you ever considered taking up knitting instead of gaming?",
    "Maybe you should quit while you're behind... which is always.",
    "I bet even your parents are disappointed in you right now.",
    "You're like a bird that keeps flying into windows. Maybe try a different approach?",
    "I'm starting to wonder if you have any skills at all.",
    "Maybe you should try a game that's more your speed. Like checkers.",
    "I've heard the phrase 'dumb as a rock', but you take it to a whole new level.",
    "You know what they say, practice makes perfect. But maybe that's not your thing.",
    "I'm pretty sure a toddler could do better than that.",
    "Maybe next time you should just stay in bed.",
    "I've seen roadkill move faster than you.",
    "Maybe you should try a game that's more your IQ level. Like tic-tac-toe.",
    "Maybe try closing your eyes and hitting random buttons. It couldn't hurt your score.",
    "You're the reason why they put 'For Dummies' on the cover of instruction manuals.",
    "I've seen better hand-eye coordination in a coma patient.",
    "You're like a broken clock. Right twice a day, but still useless.",
    "You can play better if you use your hands.",
    "Remember George you're supposed to kill them.",
    "That's embarrassing.",
    "The angel department is accepting application.",
    "Well, we're not all born to reap.",
    "Maybe try turning on your monitor.",
    "I'm sorry there is no easy mode for this game."
};

        int randomInsult = Random.Range(0, insults.Length);

        insultText.text = insults[randomInsult];
    }
}
