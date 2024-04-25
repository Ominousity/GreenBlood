import { Measurement } from "./measurement.model";

export interface Patient{
    SSN: string;
    Mail: string;
    Name: string;
    Measurements: Measurement[];
}