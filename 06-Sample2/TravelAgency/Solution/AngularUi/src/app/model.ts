export interface Trip {
  id: number;
  routeId: number;
  departureDateTime: Date;
  arrivalDateTime: Date;
  available: boolean;
  route: Route
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


export interface Ship {
  id: number;
  name: string;
}

export interface Plane {
  id: number;
  name: string;
}

export interface Hotel {
  id: number;
  name: string;
}
