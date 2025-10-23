export interface Trip {
  id: number;
  routeId: number;
  departureDateTime: Date;
  arrivalDateTime: Date;
  available: boolean;
}

export interface Route {
  id: number;
  name: string;
  steps: RouteStep[];
}

export interface RouteStep {
  no: number;
  description: string;
}

