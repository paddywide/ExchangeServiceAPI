#!/bin/bash
echo "Stopping existing .NET Core application..."
sudo systemctl stop exchange-rate.service || true
echo "Existing application stopped."


sudo rm -rf /wwwroot/exchange-rate/*

exit 0

