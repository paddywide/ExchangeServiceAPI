#!/bin/bash
echo "Stopping existing .NET Core application..."
sudo systemctl stop exchange-rate.service || true
echo "Existing application stopped."


chmod +w /wwwroot/exchange-rate/SQLite.dll 2>/dev/null || true
rm -f /wwwroot/exchange-rate/SQLite.dll 2>/dev/null || true

