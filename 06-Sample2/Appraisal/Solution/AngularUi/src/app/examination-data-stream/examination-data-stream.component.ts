import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { Examination, ExaminationDataStream } from '../model';

@Component({
  selector: 'app-examination-data-stream',
  standalone: true,
  imports: [],
  templateUrl: './examination-data-stream.component.html',
  styleUrl: './examination-data-stream.component.css'
})
export class ExaminationDataStreamComponent {
  constructor() {
  }

  @ViewChild("examinationCanvas", { static: false }) examinationCanvas!: ElementRef;
  context!: CanvasRenderingContext2D;
  canvasSize = { width: 500, height: 500 };
  @Input() examination!: Examination;
  @Input() streamNo: number | undefined;

  ngAfterViewInit(): void {
    const canvasEl: HTMLCanvasElement = this.examinationCanvas.nativeElement;
    this.canvasSize.width = canvasEl.width;
    this.canvasSize.height = canvasEl.height;
    this.context = canvasEl.getContext("2d") as CanvasRenderingContext2D;
    this.clear();
    this.renderDataStreams()
  }

  clear() {
    this.examinationCanvas.nativeElement.width = this.canvasSize.width;
    this.examinationCanvas.nativeElement.height = this.canvasSize.height;
    this.context.strokeStyle = "#FF0000";
    this.context.lineWidth = 1;
  }

  renderDataStreams() {
    if (this.examination === null) {
      return;
    }

    if (this.streamNo === undefined) {
      this.examination.dataStreams.forEach(dataStream => {
        this.renderDataStream(dataStream, dataStream.seqNo, this.examination.dataStreams.length);
      });
    }
    else {
      this.renderDataStream(this.examination.dataStreams[this.streamNo], 0, 1);
    }
  }

  renderDataStream(dataStream: ExaminationDataStream, seqNo: number, totalStreamCount: number) {

    console.log("renderDataStream", dataStream, totalStreamCount);

    const streamHeight = this.canvasSize.height / totalStreamCount;
    const streamOffset = (seqNo) * streamHeight;
    const offsetX = 0;
    const offsetY = -dataStream.minY;
    const scaleFactorX = this.canvasSize.width / dataStream.width;
    const scaleFactorY = this.canvasSize.height / totalStreamCount / dataStream.height;
    const scaleX = (x: number) => (x + offsetX) * scaleFactorX;
    const scaleY = (y: number) => this.canvasSize.height - streamOffset - (y + offsetY) * scaleFactorY;

    for (let i = 1; i < dataStream.values.length; i++) {
      const from = { x: scaleX((i - 1) * dataStream.period), y: scaleY(dataStream.values[i - 1]) };
      const to = { x: scaleX(i * dataStream.period), y: scaleY(dataStream.values[i]) };
      this.context.moveTo(from.x, from.y);
      this.context.lineTo(to.x, to.y);
      this.context.stroke();
    }
  };
}

