
docker build . -f Disco.Service.Barcodes.Api/Dockerfile -t disco.barcode.api:1.0.0

docker build . -f Disco.Service.Companies.API/Dockerfile -t disco.comapnies.api:1.0.0

docker build . -f Disco.Service.Discounts.Api/Dockerfile -t disco.discounts.api:1.0.0

docker build . -f Disco.Service.Points.Api/Dockerfile -t disco.points.api:1.0.0

docker build . -f Disco.Service.Users.Api/Dockerfile -t disco.users.api:1.0.0

docker compose up -d

docker run -p 6000:80 -p 6100:443 -d disco.barcode.api:1.0.0

docker run -p 6001:80 -p 6101:443 -d disco.comapnies.api:1.0.0

docker run -p 6002:80 -p 6102:443 -d disco.discounts.api:1.0.0

docker run -p 6003:80 -p 6103:443 -d disco.points.api:1.0.0

docker run -p 6004:80 -p 6104:443 -d disco.users.api:1.0.0

