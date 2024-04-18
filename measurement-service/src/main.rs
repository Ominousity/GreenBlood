use axum::{
    routing::{get, post, put},
    Router,
};
use axum::response::Json;
use chrono::{DateTime, Local, Utc};
use serde::{Serialize, Deserialize};




#[derive(Debug, Serialize, Deserialize)]
struct Measurement {
    id: u16,
    date: Option<DateTime<Utc>>,
    systolic: u8,
    diastolic: u8,
    patientSSN: String,
}

impl  Measurement {
    fn new(id: u16, systolic: u8, diastolic: u8, patientSSN: String) -> Measurement {
        Measurement {
            id,
            date: Some(Local::now().into()),
            systolic,
            diastolic,
            patientSSN,
        }
    }
}
async fn get_measurement() -> Json<Measurement> {
    Json(Measurement::new(1, 120, 80, "123456789".to_string()))
}

async fn post_measurement(Json(measurement): Json<Measurement>) -> Json<Measurement> {
    Json(measurement)
}

async fn put_measurement(Json(measurement): Json<Measurement>) -> Json<Measurement> {
    Json(measurement)
}


#[tokio::main]
async fn main() {
    let app = Router::new()
    .route("/measurment/:id", get(get_measurement))
    .route("/measurment", post(post_measurement))
    .route("/measurment/:id", put(put_measurement));

    let listener = tokio::net::TcpListener::bind("0.0.0.0:8080").await.unwrap();
    axum::serve(listener, app).await.unwrap();
}
