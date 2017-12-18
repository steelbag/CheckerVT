﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour {

    public Piece[,] pieces = new Piece[8, 8];
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

    private Vector2 mouseOver;

    private void Start()
    {
        GenerateBoard();
    }

    private void Update()
    {
        UpdateMouseOver();

        Debug.Log(mouseOver);
    }

    private void UpdateMouseOver()
    {
        // If its my turn

        if (!Camera.main)
        {
            Debug.Log("Unable to find main camera");
            return;
        }

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
        {
            mouseOver.x = (int)hit.point.x;
            mouseOver.y = (int)hit.point.y;
        }
        else
        {
            mouseOver.x = -1.0f;
        }   mouseOver.y = -1.0f;
    }

    private void GenerateBoard()
    {
        // Generate White Team
        for(int y = 0; y < 3; y++)
        {
            bool oddRow = y % 2 == 0;
            for(int x = 0; x < 8; x += 2)
            {
                // Generate our Piece
                GeneratePiece((oddRow? x : x+1), y);
            }
        }

        // Generate Black Team
        for (int y = 7; y > 4; y--)
        {
            bool oddRow = y % 2 == 0;
            for (int x = 0; x < 8; x += 2)
            {
                // Generate our Piece
                GeneratePiece((oddRow ? x : x + 1), y);
            }
        }
    }

    private void GeneratePiece(int x, int y)
    {
        bool isPieceWhite = y < 3;
        GameObject go = Instantiate(isPieceWhite? whitePiecePrefab : blackPiecePrefab) as GameObject;
        go.transform.SetParent(transform);
        Piece p = go.GetComponent<Piece>();
        pieces[x, y] = p;
        MovePiece(p, x, y);
    }

    private void MovePiece(Piece p, int x, int y)
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
    }
}