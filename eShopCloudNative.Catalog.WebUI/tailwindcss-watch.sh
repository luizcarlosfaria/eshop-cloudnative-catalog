#!/bin/bash

NODE_ENV=production npx tailwindcss -i ./wwwroot/css/tailwind.css -o ./wwwroot/css/site.css --minify --watch

#NODE_ENV=production npx tailwindcss -i ./wwwroot/css/tailwind.css -o ./wwwroot/css/site.css --minify
