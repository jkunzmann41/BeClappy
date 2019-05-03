using System;
using System.Collections.Generic;

public class Globals: Singleton<Globals>
{
    public static List<List<Note>> rhythms = new List<List<Note>>() {
       {new List<Note> {Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH, Note.EIGHTH }},
       {new List<Note> {Note.QUARTER, Note.EIGHTH, Note.EIGHTH, Note.QUARTER, Note.EIGHTH, Note.EIGHTH}}
    };
    public List<Note> curRhythm;

    protected Globals() { }

    public void setRhythm(int i) {
        curRhythm = rhythms[i];
    }
}