using UnityEngine;

public class Cube : MonoBehaviour
{
    private void OnLintTriggerEnter(LintCollider otherCollider)
    {
        Debug.Log("Enter: " + otherCollider.name);
    }

    private void OnLintTriggerExit(LintCollider otherCollider)
    {
        Debug.Log("Exit: " + otherCollider.name);
    }
}