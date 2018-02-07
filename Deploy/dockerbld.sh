#! /bin/bash

docker build -t macscampingarea/macsapp:latest -t macscampingarea/macsapp:prod --rm --compress .
docker login --username macscampingarea --password $(dockerPassword)
docker push --disable-content-trust macscampingarea/macsapp:prod