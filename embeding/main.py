from fastapi import FastAPI
from sentence_transformers import SentenceTransformer

app = FastAPI()
model = SentenceTransformer("all-MiniLM-L6-v2")

@app.post("/embed")
def get_embedding(text: str):
    return {"embedding": model.encode(text).tolist()}