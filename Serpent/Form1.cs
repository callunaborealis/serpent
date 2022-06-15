﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serpent
{
    public partial class SerpentGame : Form
    {

        private List<Circle> Serpent = new();
        private Circle ball = new();

        int maxWidth;
        int maxHeight;

        int score;
        int highScore;

        Random rand = new();

        bool goLeft, goRight, goUp, goDown;
        public SerpentGame()
        {
            InitializeComponent();
            new Settings(); 
        }

        private void OnGameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

        }

        private void OnGameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Settings.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Settings.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Settings.directions != "up")
            {
                goDown = true;
            }

        }

        private void OnStartButtonClick(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void OnScreenshotBtnClick(object sender, EventArgs e)
        {

        }

        private void OnGameTimerTick(object sender, EventArgs e)
        {
            // Setting directions
            if (goLeft)
            {
                Settings.directions = "left";
            }
            if (goRight)
            {
                Settings.directions = "right";
            }
            if (goDown)
            {
                Settings.directions = "down";
            }
            if (goUp)
            {
                Settings.directions = "up";
            }

            for (int i = Serpent.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.directions)
                    {
                        case "left":
                            Serpent[i].X -= 1;
                            break;
                        case "right":
                            Serpent[i].X += 1;
                            break;
                        case "up":
                            Serpent[i].Y -= 1;
                            break;
                        case "down":
                            Serpent[i].Y += 1;
                            break;
                        default:
                            break;
                    }

                    if (Serpent[i].X < 0)
                    {
                        Serpent[i].X = maxWidth;
                    }

                    if (Serpent[i].X > maxWidth)
                    {
                        Serpent[i].X = 0;
                    }

                    if (Serpent[i].Y < 0)
                    {
                        Serpent[i].Y = maxHeight;
                    }

                    if (Serpent[i].Y > maxHeight)
                    {
                        Serpent[i].Y = 0;
                    }
                } 
                else
                {
                    Serpent[i].X = Serpent[i - 1].X;
                    Serpent[i].Y = Serpent[i - 1].Y;
                }
            }

            gameCanvas.Invalidate();
        }

        private void OnScreenshotBtnClick(object sender, PaintEventArgs e)
        {

        }

        private void OnGameCanvasPaint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Brush serpentColour;

            for (int i = 0; i < Serpent.Count; i++)
            {
                if (i == 0)
                {
                    serpentColour = Brushes.Black;
                }
                else
                {
                    serpentColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(serpentColour, new Rectangle(
                    Serpent[i].X * Settings.Width,
                    Serpent[i].Y * Settings.Height,
                    Settings.Width, Settings.Height
                ));
            }

            canvas.FillEllipse(Brushes.DarkRed, new Rectangle(
                ball.X * Settings.Width,
                ball.Y * Settings.Height,
                Settings.Width, Settings.Height
            ));

        }

        private void StartNewGame()
        {
            // Allows serpent not to hug the edges too much
            maxWidth = gameCanvas.Width / Settings.Width - 1;
            maxHeight = gameCanvas.Height / Settings.Height - 1;

            Serpent.Clear();

            startButton.Enabled = false;
            screenshotButton.Enabled = false;
            score = 0;
            scoreLabel.Text = "Score: " + score;

            Circle head = new()
            {
                X = 10,
                Y = 5
            };

            Serpent.Add(head); // Add head to the serpent list of the serpent on game start

            for (int i = 0; i < 10; i++)
            {
                Circle body = new();
                Serpent.Add(body);
            }

            ball = new() { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTimer.Start();
        }

        private void SwallowBall()
        {

        }
    }
}
