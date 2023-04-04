import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {
  private log = [] as string[];

  constructor() { }

  addLog(log: string) {
    this.loadLog();
    this.log.push(log);
    this.saveLog();
  }

  getLogs() {
    return this.log;
  }

  private loadLog() {
    this.log = JSON.parse(localStorage.getItem('log') || '[]');
  }

  private saveLog() {
    localStorage.setItem('log', JSON.stringify(this.log));
  }

  getLastLog() {
    return this.log[this.log.length - 1];
  }
}
