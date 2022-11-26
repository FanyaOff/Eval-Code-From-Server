# Eval-Code-From-Server

A simple program with which you can easily send the code from the site to any computer

# How To Use

1) Rent a VPS/VDS or use free hosting, like 000webhost or another

2) Download project folder

3) Files from Server-side folder move to your server

4) If you did everything right, then by going to the site you will see this

(i'm lazy to styling this shit)

![image](https://user-images.githubusercontent.com/73064979/204075271-a268a006-9ccc-4641-866a-b7fac41824f3.png)

5) Go to Client-side, open C# Project and change link to your code.cs file

line 18:
```
string codeFile = ""; // change this link to your server file
```

line 45:
*Change this if you want to add additional libraries*

line 57:
*line 45:
*Change this if you want to add new usings or custom library usings**

6) Compile and test!

# Preview

![image](https://raw.githubusercontent.com/FanyaOff/Eval-Code-From-Server/main/preview.gif)

# If you wanna prank your friend

* In the project properties, change the application type from a console application to a Windows application. 
The application will run in the background without a console
