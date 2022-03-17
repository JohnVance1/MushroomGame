using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cauldron))]
public class CauldronEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Cauldron myTarget = (Cauldron)target;

        DrawDefaultInspector();

        if(GUILayout.Button("Random Required Ingredients"))
        {
            myTarget.RandomRequired(myTarget.randomIngredientNumber);
        }

        GUILayout.Label("Add Custom Ingredients");
        Ingredients ing = (Ingredients)EditorGUILayout.EnumPopup(Ingredients.Berry);

        int numOfIngredients = (int)GUILayout.HorizontalSlider(1, 1, 10);

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
            myTarget.RandomRequired(myTarget.randomIngredientNumber);
        }

        GUILayout.EndHorizontal();

    }

}
