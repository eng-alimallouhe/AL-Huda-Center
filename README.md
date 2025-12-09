
---

# ✅ **ثانياً: وضع ملف `docker-compose.yml` داخل README**

هذا الشكل منسّق وجاهز:

```md
## 📄 docker-compose.yml

```yaml
services:
  elasticsearch:
    image: elasticsearch:8.13.4
    container_name: elasticsearch
    environment:
      - discovery.type=single-node
      - ES_JAVA_OPTS=-Xms1g -Xmx1g
      - xpack.security.enabled=false
    ports:
      - "9200:9200"
    networks:
      - elk

  kibana:
    image: kibana:8.13.4
    container_name: kibana
    depends_on:
      - elasticsearch
    ports:
      - "5601:5601"
    environment:
      - ELASTICSEARCH_HOSTS=http://elasticsearch:9200
    networks:
      - elk

  redis:
    image: redis:7
    container_name: redis
    ports:
      - "6379:6379"
    networks:
      - backend

  redis-insight:
    image: redis/redis-stack:latest
    container_name: redis-insight
    ports:
      - "8001:8001"
    networks:
      - backend

networks:
  elk:
  backend:
