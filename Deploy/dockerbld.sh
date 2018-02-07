#! /bin/bash
PASSWORD=$1

docker build -t macscampingarea/macsapp --rm --compress .
docker login --username macscampingarea --password $PASSWORD
docker push --disable-content-trust macscampingarea/macsapp:prod