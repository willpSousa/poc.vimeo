import { Injectable } from '@angular/core';
import { VimeoVideo } from './vimeo-video.model';

@Injectable()

export class VimeoVideoLocalstorageService {
  private readonly videoKey = 'vimeo-videos';

  constructor() { }

  persist(videos: VimeoVideo[]): void {
    localStorage.setItem(this.videoKey, JSON.stringify(videos));
  }

  get(): VimeoVideo[] {
    const content = localStorage.getItem(this.videoKey);
    if (!content) {
      return [];
    }

    return JSON.parse(content);
  }
}
