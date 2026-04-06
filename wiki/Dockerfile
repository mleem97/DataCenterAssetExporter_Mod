# Build context must be the repository root (see docker-compose.yml).
# Use /app as the Docusaurus project root (package.json here) so default cwd /app works;
# docs live at /docs, matching docusaurus.config.js path: '../docs' from this folder.
FROM node:20-alpine
WORKDIR /app
COPY wiki/docker-entrypoint.sh /usr/local/bin/docker-entrypoint.sh
RUN chmod +x /usr/local/bin/docker-entrypoint.sh
COPY wiki/package.json wiki/package-lock.json ./
RUN npm ci
COPY wiki/ ./
COPY docs/ /docs/
ENTRYPOINT ["docker-entrypoint.sh"]
EXPOSE 3000
CMD ["npm", "run", "start", "--", "--host", "0.0.0.0", "--port", "3000"]
