export interface Script {
  id: number;
  name: string;
  description: string;
}

export interface Competition {
  id: number;
  description: string;
  active: boolean;
  scripts: Script[];
}

export interface Vote {
  scriptId: number;
  rate: number;
}

export interface VoteResult {
  scriptId: number;
  name: string;
  count1: number;
  count2: number;
  count3: number;
  count4: number;
  count5: number;
}

export interface CompetitionVoteResult {
  id: number;
  name: string;
  votes: VoteResult[];
}

