# Postcard
Postcard Creator App

This is a postcard creator application designed in WPF.
Designed to work with .Net 4.7.2 x86

It uses WebEye.Controls.Wpf.WebCameraControl. To run in Visual Studio make sure to install package 
Install-Package WebEye.Controls.Wpf.WebCameraControl -Version 1.0.3
https://www.codeproject.com/Articles/330177/Yet-another-Web-Camera-control
This package was selected for simplicity and ease of use.

General Guide after installing Application.
Navigate back on top left.

On load screen select:
1. Capture Image
2. View Postcards

On capature image:
Start camera and take photo, when photo happy with photo, click add text.

Add text Screen:
Fill in greeting and location, select where text will appear.

Fill in relevant email info and send email. 
Note: only works with outlook

View sent post cards from load screen, View Postcards.
Most recent 6 cards are shown.

Notes:
Using UWP instead of WPF would allow for easier built in webcam functionality as well as geotagging and use of outlook app.

