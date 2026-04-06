FROM node:20-alpine

WORKDIR /repo/wiki

COPY wiki/package.json wiki/package-lock.json* ./
RUN npm install

COPY wiki/ .
COPY docs /repo/docs

EXPOSE 3000

CMD ["npm", "run", "start"]
