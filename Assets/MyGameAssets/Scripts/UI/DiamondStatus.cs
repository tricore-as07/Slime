using UnityEngine;
using UnityEngine.UI;

public class DiamondStatus : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color getColor;
    [SerializeField] Color notGetColor;

    public void UpdateAcquisitionStatus(bool status)
    {
        if(status)
        {
            image.color = getColor;
        }
        else
        {
            image.color = notGetColor;
        }
    }
}
