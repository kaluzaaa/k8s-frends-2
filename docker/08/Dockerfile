FROM ubuntu

ENV ENV=LOCAL
ENV API=http://localhost:9090

WORKDIR /app

COPY src/ .

# tutaj jakis kod zrodlowy
# RUN dotnet build

RUN chmod +x script.sh

CMD [ "./script.sh" ]