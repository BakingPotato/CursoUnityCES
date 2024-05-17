using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager_Chess : MonoBehaviour
{
    [Header("Balanceo")]
    public float[] time_to_fall = { 0.5f, 1, 1.5f, 2, 2.5f, 3, 3.5f };
    public float time_falling = 2;
    public float time_to_fall_increment = 0.2f;
    public float cooldown = 1;
    public float cooldown_after_falling = 2;

    public Material Grey;

    List<ChessSquare> toFall = new List<ChessSquare>();


	public static GameManager_Chess Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("Null GM");
			return _instance;
		}
	}
	private static GameManager_Chess _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartCoroutine(difficulty_increase());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator difficulty_increase()
    {
        yield return new WaitForSeconds(Random.Range(15, 30));
        yield return makeArenaSmaller("81");
        yield return new WaitForSeconds(Random.Range(15, 30));
        yield return makeArenaSmaller("72");

    }

    IEnumerator makeArenaSmaller(string initial)
    {
        List<ChessSquare> toFall = new List<ChessSquare>();
        ChessSquare cs = GameObject.Find(initial).GetComponent<ChessSquare>();

        toFall.Add(cs);
        cs.GetComponent<MeshRenderer>().material = Grey;
        cs.terminated = true;
        yield return new WaitForSeconds(0.1f);

        while (cs.box_right)
        {
            cs = cs.box_right.GetComponent<ChessSquare>();
            cs.terminated = true;
            toFall.Add(cs);
            cs.GetComponent<MeshRenderer>().material = Grey;
            yield return new WaitForSeconds(0.1f);
        }

        while (cs.box_down)
        {
            cs = cs.box_down.GetComponent<ChessSquare>();
            cs.terminated = true;
            toFall.Add(cs);
            cs.GetComponent<MeshRenderer>().material = Grey;
            yield return new WaitForSeconds(0.1f);
        }

        while (cs.box_left)
        {
            cs = cs.box_left.GetComponent<ChessSquare>();
            cs.terminated = true;
            toFall.Add(cs);
            cs.GetComponent<MeshRenderer>().material = Grey;
            yield return new WaitForSeconds(0.1f);
        }

        while (cs.box_up)
        {
            cs = cs.box_up.GetComponent<ChessSquare>();
            cs.terminated = true;
            toFall.Add(cs);
            cs.GetComponent<MeshRenderer>().material = Grey;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);

        foreach(ChessSquare csq in toFall)
        {
            csq.instantFall();
        }
    }
}
