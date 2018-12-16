using UnityEngine;
using System.Collections;
public class Meteor : MonoBehaviour {

    private float speed=5;
    void Update()
    {
        transform.Translate(new Vector2(0, -1) * Time.deltaTime * speed);
        dealPos();
    }

    void dealPos()
    {
        Vector2 v = Grid.roundVec2(this.transform.position);
        if (Grid.grid[(int)v.x, (int)v.y] != null)
        {
            Grid.deleteSingle(v);
            Destroy(this.gameObject);
        }
        if (this.transform.position.y < 0)
            Destroy(this.gameObject);
    }
}
