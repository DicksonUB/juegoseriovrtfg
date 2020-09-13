using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController :QuestionController
{
    public override void QuestionNotPassed()
    {
        DeactivateCanvas();
        gc.DeletePart(gameObject);
    }
    public override void QuestionPassed()
    {
        DeactivateCanvas();
        gc.FoundObject(gameObject.name, type, gameObject.transform.position, gameObject);
    }
}
