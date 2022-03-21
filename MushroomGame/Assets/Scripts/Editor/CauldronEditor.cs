using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cauldron))]
public class CauldronEditor : Editor
{
    public Ingredients ing;
    public int numOfIngredients;

    public override void OnInspectorGUI()
    {
        Cauldron myTarget = (Cauldron)target;

        DrawDefaultInspector();

        if(GUILayout.Button("Random Required Ingredients"))
        {
            myTarget.RandomRequired(myTarget.randomIngredientNumber);
        }

        GUILayout.Label("Add Custom Ingredients");
        ing = (Ingredients)EditorGUILayout.EnumPopup("Ingredient type", ing);

        numOfIngredients = (int)EditorGUILayout.Slider(numOfIngredients, 1, 10);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Ingredient"))
        {
            for (int i = 0; i < numOfIngredients; i++)
            {
                myTarget.AddIngredient(ing);
            }
        }

        if (GUILayout.Button("Subtract Ingredient"))
        {
            for (int i = 0; i < numOfIngredients; i++)
            {
                myTarget.RemoveIngredient(ing);
            }
        }

        GUILayout.EndHorizontal();

    }

}
