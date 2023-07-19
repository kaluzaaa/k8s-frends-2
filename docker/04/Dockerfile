FROM nginx

LABEL version="1.0"
LABEL description="This is a custom nginx image"
LABEL maintainer="lukasz@kaluzny.io"

COPY index.html /usr/share/nginx/html/

RUN echo "Hello World" > /usr/share/nginx/html/index.html
RUN apt-get update && apt-get install -y wget