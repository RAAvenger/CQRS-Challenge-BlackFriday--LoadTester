# BlackFriday problem for CQRS challenge

## template app with observability tools

to run template code you can run

```bash
docker compose up -d --build
```

but you will need a postgres database with sample data.

## load tester

to run load tester you can run

```bash
docker compose -f .\load-tester-docker-compose.yml up -d --build
```

you can specify the endpoint that your black friday app is listening to and number of users in `load-tester-docker-compose.yml`.