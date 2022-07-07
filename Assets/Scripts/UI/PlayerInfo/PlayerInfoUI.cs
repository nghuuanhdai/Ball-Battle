using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private PlayerInfoText playerInfoText;
    [SerializeField] private PlayerEnergyBar playerEnergyBar;

    [SerializeField] private Color _accentColor = Color.gray;
    [SerializeField] private string _playerName = "undefined";
    [SerializeField] private TeamPlayMode _playerPosition;
    [SerializeField] private int _maxEnergy = 5;
    [SerializeField] private float _energy = 0;

    public Color AccentColor { get => _accentColor; set {_accentColor = value; OnAccentColorChanged();} }
    public string PlayerName { get => _playerName; set {_playerName = value; OnPlayerNameChanged();} }
    public TeamPlayMode PlayerPosition { get => _playerPosition; set {_playerPosition = value; OnPositionChanged();} }
    public int MaxEnergy { get => _maxEnergy; set {_maxEnergy = value; OnMaxEnergyChanged();} }
    public float Energy { get => _energy; set {_energy = value; OnEnergyChanged();} }

    private void OnEnergyChanged()
    {
        if(playerEnergyBar)
            playerEnergyBar.Energy = Energy;
    }

    private void OnMaxEnergyChanged()
    {
        if(playerEnergyBar)
            playerEnergyBar.MaxEnergy = MaxEnergy;
    }

    private void OnAccentColorChanged()
    {
        if(playerInfoText)
            playerInfoText.TextColor = AccentColor;
        if(playerEnergyBar)
            playerEnergyBar.Color = AccentColor;
    }

    private void OnPlayerNameChanged()
    {
        if(playerInfoText)
            playerInfoText.Text = GetPlayerInfoText();
    }

    private void OnPositionChanged()
    {
        if(playerInfoText)
            playerInfoText.Text = GetPlayerInfoText();
    }

    private string GetPlayerInfoText()
    {
        return $"{PlayerName} ({PlayerPosition})";
    }
}