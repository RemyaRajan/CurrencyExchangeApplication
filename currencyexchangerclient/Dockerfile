# Use a Node 16 base image
FROM node:latest 
WORKDIR /app
EXPOSE 3000

COPY . .
RUN npm install 
# Build the app
RUN npm run build
CMD [ "npx", "serve", "build" ]