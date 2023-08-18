import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { VimeoVideo } from './vimeo-video.model';
import { VimeoVideoLocalstorageService } from './vimeo-video-localstorage.service';

@Component({
  selector: 'app-vimeo-upload',
  templateUrl: './vimeo-upload.component.html',
  styleUrls: ['./vimeo-upload.component.scss']
})
export class VimeoUploadComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  uploading = false;
  lastUploadError = false;
  videos: Array<VimeoVideo> = [];

  statusMap = {
    complete: 'complete',
    error: 'error',
    in_progress: 'in_progress'
  }

  statusMapCard: { [id: string]: string } = {
    [this.statusMap.complete]: 'border-success',
    [this.statusMap.error]: 'border-error',
    [this.statusMap.in_progress]: 'border-secondary'
  };

  constructor(private http: HttpClient, private videoStorage: VimeoVideoLocalstorageService) {
    this.videos = videoStorage.get();
  }

  upload(event: any): void {
    const file: File = event.target.files[0];
    if (file && !this.uploading) {
      const formData = new FormData();

      formData.append('file_data', file);

      this.uploading = true;
      this.lastUploadError = false;

      this.http.post<VimeoVideo>('/api/vimeo', formData)
        .subscribe({
          next: (video: VimeoVideo) => {
            video.show = true;
            this.uploading = false;
            this.add(video);
          },
          error: () => {
            this.lastUploadError = true;
            this.uploading = false;
          }
        });

    }
    this.fileInput.nativeElement.value = '';
  }

  private add(video: VimeoVideo): void {
    this.videos.push(video);
    this.persist();
  }

  private persist() {
    this.videoStorage.persist(this.videos);
  }

  checkStatus(video: VimeoVideo): void {

    if (!video) {
      return;
    }

    video.show = false;

    this.http.get<VimeoVideo>(`/api/vimeo/${this.extractId(video)}/status`)
      .subscribe({
        next: (response: VimeoVideo) => {
          video.transcode = response.transcode;
          video.upload = response.upload;
          video.show = true;
          this.persist();
        },
        error: () => video.show = true
      });
  }

  delete(video: VimeoVideo, index: number): void {
    this.http.delete('/api/vimeo/' + this.extractId(video)).subscribe({
      next: () => {
        this.videos.splice(index, 1)
        this.persist();
      }
    });
  }

  private extractId(video: VimeoVideo): number {
    return Number.parseInt(video.uri.split('/')[2], 10);
  }
}
