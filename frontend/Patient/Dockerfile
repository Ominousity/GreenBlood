FROM node:20.1 as build
WORKDIR /app
COPY /frontend/Patient/package.json ./package.json
RUN npm install --no-cache
RUN npm install -g @angular/cli
COPY /frontend/Patient/ ./
RUN ng build
FROM nginx:alpine
COPY --from=build /app/dist/green-blood/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]