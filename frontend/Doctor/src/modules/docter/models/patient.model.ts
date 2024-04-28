import { Measurement } from "./measurement.model";

export interface Patient{
    id: number;
    ssn: string;
    email: string;
    name: string;
    measurements: Measurement[];
}