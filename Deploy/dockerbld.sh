#! /bin/bash
PASSWORD=$1

cd ../
docker login --username macscampingarea --password $PASSWORD
docker build -t macscampingarea/macsapp --rm --compress .
docker push --disable-content-trust macscampingarea/macsapp:prod