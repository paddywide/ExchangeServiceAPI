#!/bin/bash
# Ensure correct permissions
sudo chown -R www-data:www-data /wwwroot/exchange-rate
sudo chmod -R 755 /wwwroot/exchange-rate

echo "Starting .NET Core application..."
sudo systemctl start exchange-rate.service
echo "Application started."
