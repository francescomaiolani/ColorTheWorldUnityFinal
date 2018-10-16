using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//CLASSE CHE CALCOLA L'ANGOLO E LA FORZA CON CUI LANCIARE IL BALOON SULLA BASE DEL DRAG EFFETTUATO
//DISEGNA ANCHE LA TRAIETTORIA DEL BALOON 
public class ShootBaloonButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    public Vector2 startTouch;
    public Vector2 endTouch;
    public Vector2 intermediateTouch;
    public float maxDragDistance;

    public Animator playerAnimator;

    public float minAngle;
    public float maxAngle;

    public Baloon baloon;
    public PlayerShoot player;
    public LineRenderer trajectory;
    int resolution = 16;
    float angle;
    float predictedVelocity;
    float g;

    private void Awake()
    {
        g = Mathf.Abs(Physics2D.gravity.y);
        maxAngle = maxAngle * Mathf.Deg2Rad;
        minAngle = minAngle * Mathf.Deg2Rad;
    }

    void OnValidate() {

    }
    public void OnBeginDrag(PointerEventData data) {
        baloon = player.actualBaloon;
        startTouch = data.position;
        playerAnimator.SetTrigger("Charge");
    }

     public void OnDrag(PointerEventData data) {
        intermediateTouch = data.position;
        angle = (Mathf.Atan2(intermediateTouch.y - startTouch.y, intermediateTouch.x - startTouch.x) + Mathf.PI);

        if (angle <= maxAngle && angle >= Mathf.PI)
            angle = maxAngle;
        if (angle >= minAngle && angle < Mathf.PI)
            angle = minAngle;

        
        float distance = Vector2.Distance(startTouch, intermediateTouch);
        predictedVelocity = baloon.GetMaxSpeed() * ConvertDistance(distance);

        if (predictedVelocity != 0)
            DrawTrajectory(predictedVelocity, angle);
     }

    public void OnEndDrag(PointerEventData data)
    {
        endTouch = data.position;
        float angle = (Mathf.Atan2(endTouch.y - startTouch.y, endTouch.x - startTouch.x) + Mathf.PI);
        if (angle <= maxAngle && angle >= Mathf.PI)
            angle = maxAngle;
        if (angle >= minAngle && angle < Mathf.PI)
            angle = minAngle;
        float distance = Vector2.Distance(startTouch, endTouch);
        //A STO PUNTO SPARA IL GAVETTONE
        player.ShootBaloon(angle, ConvertDistance(distance));
        playerAnimator.SetTrigger("Launch");
        ClearTrajectory();

    }

    float ConvertDistance(float initialDistance) {
        if (initialDistance >= maxDragDistance)
            return 1;
        else
            return initialDistance / maxDragDistance;
    }

    void DrawTrajectory(float baloonVelocity, float angle) {
        trajectory.SetVertexCount(resolution + 1);
        trajectory.SetPositions(CalculateArcArray());
        
    }

    Vector3[] CalculateArcArray() {
        Vector3[] arcArray = new Vector3[resolution + 1];
        float maxDistance = (predictedVelocity * predictedVelocity * Mathf.Sin(2 * angle)) / g;


        for (int i = 0; i <= resolution; i++) {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance) {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(angle) - ((g * x * x) / (2 * predictedVelocity * predictedVelocity * Mathf.Cos(angle) * Mathf.Cos(angle)));
        return new Vector3(x, y) + player.transform.position;
    }

    void ClearTrajectory() {
        trajectory.SetVertexCount(0);
    }

}
