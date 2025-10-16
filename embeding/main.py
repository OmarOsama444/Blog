from fastapi import FastAPI
from sentence_transformers import SentenceTransformer

app = FastAPI()
model = SentenceTransformer("BAAI/bge-base-en-v1.5") 

@app.post("/embed")
def get_embedding(text: str):
    return {"embedding": model.encode(text).tolist()}