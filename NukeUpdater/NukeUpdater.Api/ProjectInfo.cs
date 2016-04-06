﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NukeUpdater.Api
{
    public class ProjectInfo
    {
#if EMBED
        internal static byte[] EXE_DATA = new byte[] { 77, 90, 144, 0, 3, 0, 0, 0, 4, 0, 0, 0, 255, 255, 0, 0, 184, 0, 0, 0, 0, 0, 0, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 0, 0, 14, 31, 186, 14, 0, 180, 9, 205, 33, 184, 1, 76, 205, 33, 84, 104, 105, 115, 32, 112, 114, 111, 103, 114, 97, 109, 32, 99, 97, 110, 110, 111, 116, 32, 98, 101, 32, 114, 117, 110, 32, 105, 110, 32, 68, 79, 83, 32, 109, 111, 100, 101, 46, 13, 13, 10, 36, 0, 0, 0, 0, 0, 0, 0, 80, 69, 0, 0, 76, 1, 3, 0, 76, 220, 4, 87, 0, 0, 0, 0, 0, 0, 0, 0, 224, 0, 2, 1, 11, 1, 11, 0, 0, 16, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 190, 47, 0, 0, 0, 32, 0, 0, 0, 64, 0, 0, 0, 0, 64, 0, 0, 32, 0, 0, 0, 2, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 3, 0, 96, 133, 0, 0, 16, 0, 0, 16, 0, 0, 0, 0, 16, 0, 0, 16, 0, 0, 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 108, 47, 0, 0, 79, 0, 0, 0, 0, 64, 0, 0, 112, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 96, 0, 0, 12, 0, 0, 0, 52, 46, 0, 0, 28, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 32, 0, 0, 72, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 46, 116, 101, 120, 116, 0, 0, 0, 196, 15, 0, 0, 0, 32, 0, 0, 0, 16, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 96, 46, 114, 115, 114, 99, 0, 0, 0, 112, 5, 0, 0, 0, 64, 0, 0, 0, 6, 0, 0, 0, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 0, 64, 46, 114, 101, 108, 111, 99, 0, 0, 12, 0, 0, 0, 0, 96, 0, 0, 0, 2, 0, 0, 0, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 0, 66, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 160, 47, 0, 0, 0, 0, 0, 0, 72, 0, 0, 0, 2, 0, 5, 0, 28, 34, 0, 0, 24, 12, 0, 0, 3, 0, 2, 0, 1, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 19, 48, 3, 0, 166, 1, 0, 0, 1, 0, 0, 17, 2, 142, 105, 22, 49, 15, 2, 22, 154, 114, 1, 0, 0, 112, 40, 17, 0, 0, 10, 43, 1, 22, 10, 40, 18, 0, 0, 10, 111, 19, 0, 0, 10, 40, 20, 0, 0, 10, 11, 7, 126, 21, 0, 0, 10, 40, 22, 0, 0, 10, 12, 8, 40, 23, 0, 0, 10, 45, 42, 114, 13, 0, 0, 112, 40, 24, 0, 0, 10, 114, 121, 0, 0, 112, 40, 24, 0, 0, 10, 40, 25, 0, 0, 10, 114, 147, 0, 0, 112, 40, 24, 0, 0, 10, 40, 26, 0, 0, 10, 38, 42, 8, 40, 27, 0, 0, 10, 40, 1, 0, 0, 43, 13, 9, 7, 111, 29, 0, 0, 10, 114, 187, 0, 0, 112, 127, 1, 0, 0, 4, 114, 229, 0, 0, 112, 40, 30, 0, 0, 10, 40, 31, 0, 0, 10, 40, 24, 0, 0, 10, 9, 111, 32, 0, 0, 10, 45, 94, 114, 235, 0, 0, 112, 9, 111, 33, 0, 0, 10, 140, 27, 0, 0, 1, 40, 34, 0, 0, 10, 40, 24, 0, 0, 10, 40, 25, 0, 0, 10, 9, 9, 111, 33, 0, 0, 10, 111, 35, 0, 0, 10, 111, 36, 0, 0, 10, 19, 4, 9, 17, 4, 111, 37, 0, 0, 10, 9, 20, 17, 4, 111, 38, 0, 0, 10, 9, 23, 111, 39, 0, 0, 10, 9, 17, 4, 111, 40, 0, 0, 10, 111, 41, 0, 0, 10, 9, 111, 42, 0, 0, 10, 42, 114, 65, 1, 0, 112, 40, 24, 0, 0, 10, 9, 111, 43, 0, 0, 10, 111, 44, 0, 0, 10, 19, 5, 9, 111, 32, 0, 0, 10, 44, 60, 9, 111, 33, 0, 0, 10, 17, 5, 111, 33, 0, 0, 10, 50, 45, 6, 45, 42, 114, 109, 1, 0, 112, 40, 24, 0, 0, 10, 114, 185, 1, 0, 112, 40, 24, 0, 0, 10, 40, 25, 0, 0, 10, 114, 147, 0, 0, 112, 40, 24, 0, 0, 10, 40, 26, 0, 0, 10, 38, 42, 9, 9, 111, 33, 0, 0, 10, 111, 35, 0, 0, 10, 111, 36, 0, 0, 10, 19, 6, 9, 17, 5, 111, 45, 0, 0, 10, 111, 36, 0, 0, 10, 19, 7, 9, 17, 7, 111, 37, 0, 0, 10, 9, 17, 6, 17, 7, 111, 38, 0, 0, 10, 9, 23, 111, 39, 0, 0, 10, 9, 17, 7, 111, 40, 0, 0, 10, 111, 41, 0, 0, 10, 9, 111, 42, 0, 0, 10, 42, 62, 35, 0, 0, 0, 0, 0, 0, 240, 63, 128, 1, 0, 0, 4, 42, 30, 2, 40, 46, 0, 0, 10, 42, 0, 0, 66, 83, 74, 66, 1, 0, 1, 0, 0, 0, 0, 0, 12, 0, 0, 0, 118, 52, 46, 48, 46, 51, 48, 51, 49, 57, 0, 0, 0, 0, 5, 0, 108, 0, 0, 0, 28, 3, 0, 0, 35, 126, 0, 0, 136, 3, 0, 0, 160, 4, 0, 0, 35, 83, 116, 114, 105, 110, 103, 115, 0, 0, 0, 0, 40, 8, 0, 0, 40, 2, 0, 0, 35, 85, 83, 0, 80, 10, 0, 0, 16, 0, 0, 0, 35, 71, 85, 73, 68, 0, 0, 0, 96, 10, 0, 0, 184, 1, 0, 0, 35, 66, 108, 111, 98, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 1, 87, 21, 2, 8, 9, 8, 0, 0, 0, 250, 37, 51, 0, 22, 0, 0, 1, 0, 0, 0, 29, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 46, 0, 0, 0, 14, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 10, 0, 1, 0, 0, 0, 0, 0, 6, 0, 70, 0, 63, 0, 6, 0, 127, 0, 101, 0, 6, 0, 170, 0, 152, 0, 6, 0, 193, 0, 152, 0, 6, 0, 222, 0, 152, 0, 6, 0, 253, 0, 152, 0, 6, 0, 22, 1, 152, 0, 6, 0, 47, 1, 152, 0, 6, 0, 74, 1, 152, 0, 6, 0, 101, 1, 152, 0, 6, 0, 157, 1, 126, 1, 6, 0, 177, 1, 126, 1, 6, 0, 191, 1, 152, 0, 6, 0, 216, 1, 152, 0, 6, 0, 8, 2, 245, 1, 63, 0, 28, 2, 0, 0, 6, 0, 75, 2, 43, 2, 6, 0, 107, 2, 43, 2, 6, 0, 137, 2, 63, 0, 6, 0, 156, 2, 152, 0, 6, 0, 205, 2, 195, 2, 10, 0, 243, 2, 227, 2, 6, 0, 23, 3, 195, 2, 6, 0, 35, 3, 63, 0, 14, 0, 90, 3, 74, 3, 6, 0, 137, 3, 63, 0, 6, 0, 190, 3, 63, 0, 6, 0, 219, 3, 196, 3, 10, 0, 226, 3, 227, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 16, 0, 30, 0, 38, 0, 5, 0, 1, 0, 1, 0, 17, 0, 77, 0, 10, 0, 80, 32, 0, 0, 0, 0, 145, 0, 85, 0, 13, 0, 1, 0, 18, 34, 0, 0, 0, 0, 134, 24, 90, 0, 19, 0, 2, 0, 2, 34, 0, 0, 0, 0, 145, 24, 153, 4, 83, 0, 2, 0, 0, 0, 1, 0, 96, 0, 17, 0, 90, 0, 23, 0, 25, 0, 90, 0, 23, 0, 33, 0, 90, 0, 23, 0, 41, 0, 90, 0, 23, 0, 49, 0, 90, 0, 23, 0, 57, 0, 90, 0, 23, 0, 65, 0, 90, 0, 23, 0, 73, 0, 90, 0, 23, 0, 81, 0, 90, 0, 23, 0, 89, 0, 90, 0, 28, 0, 97, 0, 90, 0, 23, 0, 105, 0, 90, 0, 23, 0, 113, 0, 90, 0, 23, 0, 121, 0, 90, 0, 33, 0, 137, 0, 90, 0, 39, 0, 145, 0, 90, 0, 19, 0, 153, 0, 144, 2, 44, 0, 161, 0, 165, 2, 50, 0, 161, 0, 182, 2, 55, 0, 169, 0, 210, 2, 59, 0, 177, 0, 255, 2, 64, 0, 169, 0, 15, 3, 67, 0, 185, 0, 28, 3, 73, 0, 193, 0, 43, 3, 78, 0, 193, 0, 43, 3, 83, 0, 193, 0, 53, 3, 87, 0, 185, 0, 62, 3, 59, 0, 201, 0, 102, 3, 100, 0, 177, 0, 120, 3, 23, 0, 209, 0, 144, 3, 112, 0, 153, 0, 153, 3, 67, 0, 177, 0, 160, 3, 117, 0, 177, 0, 179, 3, 121, 0, 153, 0, 153, 3, 125, 0, 177, 0, 237, 3, 131, 0, 12, 0, 2, 4, 148, 0, 177, 0, 13, 4, 153, 0, 177, 0, 38, 4, 159, 0, 177, 0, 57, 4, 28, 0, 233, 0, 76, 4, 121, 0, 177, 0, 89, 4, 39, 0, 177, 0, 100, 4, 19, 0, 177, 0, 105, 4, 167, 0, 20, 0, 2, 4, 148, 0, 177, 0, 126, 4, 183, 0, 9, 0, 90, 0, 19, 0, 46, 0, 11, 0, 210, 0, 46, 0, 19, 0, 28, 1, 46, 0, 27, 0, 49, 1, 46, 0, 35, 0, 49, 1, 46, 0, 43, 0, 49, 1, 46, 0, 51, 0, 28, 1, 46, 0, 59, 0, 55, 1, 46, 0, 67, 0, 49, 1, 46, 0, 83, 0, 49, 1, 46, 0, 91, 0, 79, 1, 46, 0, 107, 0, 121, 1, 46, 0, 115, 0, 134, 1, 46, 0, 123, 0, 143, 1, 46, 0, 131, 0, 152, 1, 194, 0, 141, 0, 176, 0, 4, 128, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 38, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 227, 2, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 91, 0, 74, 3, 0, 0, 0, 0, 57, 0, 107, 0, 0, 0, 0, 60, 77, 111, 100, 117, 108, 101, 62, 0, 78, 117, 107, 101, 85, 112, 100, 97, 116, 101, 114, 46, 65, 112, 112, 46, 101, 120, 101, 0, 80, 114, 111, 103, 114, 97, 109, 0, 78, 117, 107, 101, 85, 112, 100, 97, 116, 101, 114, 46, 65, 112, 112, 0, 109, 115, 99, 111, 114, 108, 105, 98, 0, 83, 121, 115, 116, 101, 109, 0, 79, 98, 106, 101, 99, 116, 0, 86, 101, 114, 115, 105, 111, 110, 0, 77, 97, 105, 110, 0, 46, 99, 116, 111, 114, 0, 97, 114, 103, 115, 0, 83, 121, 115, 116, 101, 109, 46, 82, 117, 110, 116, 105, 109, 101, 46, 86, 101, 114, 115, 105, 111, 110, 105, 110, 103, 0, 84, 97, 114, 103, 101, 116, 70, 114, 97, 109, 101, 119, 111, 114, 107, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 83, 121, 115, 116, 101, 109, 46, 82, 101, 102, 108, 101, 99, 116, 105, 111, 110, 0, 65, 115, 115, 101, 109, 98, 108, 121, 84, 105, 116, 108, 101, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 68, 101, 115, 99, 114, 105, 112, 116, 105, 111, 110, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 67, 111, 110, 102, 105, 103, 117, 114, 97, 116, 105, 111, 110, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 67, 111, 109, 112, 97, 110, 121, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 80, 114, 111, 100, 117, 99, 116, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 67, 111, 112, 121, 114, 105, 103, 104, 116, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 84, 114, 97, 100, 101, 109, 97, 114, 107, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 67, 117, 108, 116, 117, 114, 101, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 83, 121, 115, 116, 101, 109, 46, 82, 117, 110, 116, 105, 109, 101, 46, 73, 110, 116, 101, 114, 111, 112, 83, 101, 114, 118, 105, 99, 101, 115, 0, 67, 111, 109, 86, 105, 115, 105, 98, 108, 101, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 71, 117, 105, 100, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 86, 101, 114, 115, 105, 111, 110, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 115, 101, 109, 98, 108, 121, 70, 105, 108, 101, 86, 101, 114, 115, 105, 111, 110, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 83, 121, 115, 116, 101, 109, 46, 68, 105, 97, 103, 110, 111, 115, 116, 105, 99, 115, 0, 68, 101, 98, 117, 103, 103, 97, 98, 108, 101, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 68, 101, 98, 117, 103, 103, 105, 110, 103, 77, 111, 100, 101, 115, 0, 83, 121, 115, 116, 101, 109, 46, 82, 117, 110, 116, 105, 109, 101, 46, 67, 111, 109, 112, 105, 108, 101, 114, 83, 101, 114, 118, 105, 99, 101, 115, 0, 67, 111, 109, 112, 105, 108, 97, 116, 105, 111, 110, 82, 101, 108, 97, 120, 97, 116, 105, 111, 110, 115, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 82, 117, 110, 116, 105, 109, 101, 67, 111, 109, 112, 97, 116, 105, 98, 105, 108, 105, 116, 121, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 83, 116, 114, 105, 110, 103, 0, 111, 112, 95, 69, 113, 117, 97, 108, 105, 116, 121, 0, 65, 115, 115, 101, 109, 98, 108, 121, 0, 71, 101, 116, 69, 110, 116, 114, 121, 65, 115, 115, 101, 109, 98, 108, 121, 0, 103, 101, 116, 95, 76, 111, 99, 97, 116, 105, 111, 110, 0, 83, 121, 115, 116, 101, 109, 46, 73, 79, 0, 80, 97, 116, 104, 0, 71, 101, 116, 68, 105, 114, 101, 99, 116, 111, 114, 121, 78, 97, 109, 101, 0, 78, 117, 107, 101, 85, 112, 100, 97, 116, 101, 114, 46, 65, 112, 105, 0, 80, 114, 111, 106, 101, 99, 116, 73, 110, 102, 111, 0, 80, 114, 111, 106, 101, 99, 116, 73, 110, 102, 111, 70, 105, 108, 101, 0, 67, 111, 109, 98, 105, 110, 101, 0, 70, 105, 108, 101, 0, 69, 120, 105, 115, 116, 115, 0, 67, 111, 110, 115, 111, 108, 101, 0, 87, 114, 105, 116, 101, 76, 105, 110, 101, 0, 82, 101, 97, 100, 76, 105, 110, 101, 0, 82, 101, 97, 100, 65, 108, 108, 84, 101, 120, 116, 0, 78, 101, 119, 116, 111, 110, 115, 111, 102, 116, 46, 74, 115, 111, 110, 0, 74, 115, 111, 110, 67, 111, 110, 118, 101, 114, 116, 0, 68, 101, 115, 101, 114, 105, 97, 108, 105, 122, 101, 79, 98, 106, 101, 99, 116, 0, 73, 110, 105, 116, 105, 97, 108, 105, 122, 101, 67, 108, 105, 101, 110, 116, 0, 68, 111, 117, 98, 108, 101, 0, 84, 111, 83, 116, 114, 105, 110, 103, 0, 67, 111, 110, 99, 97, 116, 0, 103, 101, 116, 95, 70, 105, 110, 105, 115, 104, 101, 100, 85, 112, 100, 97, 116, 101, 0, 103, 101, 116, 95, 76, 97, 116, 101, 115, 116, 0, 73, 110, 116, 51, 50, 0, 83, 121, 115, 116, 101, 109, 46, 84, 104, 114, 101, 97, 100, 105, 110, 103, 46, 84, 97, 115, 107, 115, 0, 84, 97, 115, 107, 96, 49, 0, 85, 112, 100, 97, 116, 101, 73, 110, 102, 111, 0, 71, 101, 116, 86, 101, 114, 115, 105, 111, 110, 70, 114, 111, 109, 83, 101, 114, 118, 101, 114, 0, 103, 101, 116, 95, 82, 101, 115, 117, 108, 116, 0, 68, 111, 119, 110, 108, 111, 97, 100, 85, 112, 100, 97, 116, 101, 70, 114, 111, 109, 83, 101, 114, 118, 101, 114, 0, 68, 111, 85, 112, 100, 97, 116, 101, 70, 114, 111, 109, 83, 101, 114, 118, 101, 114, 0, 115, 101, 116, 95, 70, 105, 110, 105, 115, 104, 101, 100, 85, 112, 100, 97, 116, 101, 0, 103, 101, 116, 95, 82, 101, 118, 105, 115, 105, 111, 110, 0, 115, 101, 116, 95, 76, 97, 116, 101, 115, 116, 0, 83, 97, 118, 101, 0, 71, 101, 116, 80, 114, 111, 106, 101, 99, 116, 70, 114, 111, 109, 83, 101, 114, 118, 101, 114, 0, 71, 101, 116, 76, 97, 116, 101, 115, 116, 86, 101, 114, 115, 105, 111, 110, 70, 114, 111, 109, 83, 101, 114, 118, 101, 114, 0, 46, 99, 99, 116, 111, 114, 0, 0, 11, 102, 0, 111, 0, 114, 0, 99, 0, 101, 0, 0, 107, 65, 0, 112, 0, 112, 0, 108, 0, 105, 0, 99, 0, 97, 0, 116, 0, 105, 0, 111, 0, 110, 0, 32, 0, 100, 0, 105, 0, 100, 0, 32, 0, 110, 0, 111, 0, 116, 0, 32, 0, 99, 0, 114, 0, 101, 0, 97, 0, 116, 0, 101, 0, 32, 0, 100, 0, 101, 0, 102, 0, 97, 0, 117, 0, 108, 0, 116, 0, 32, 0, 78, 0, 117, 0, 107, 0, 101, 0, 32, 0, 85, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 114, 0, 32, 0, 102, 0, 105, 0, 108, 0, 101, 0, 115, 0, 0, 25, 67, 0, 97, 0, 110, 0, 39, 0, 116, 0, 32, 0, 117, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 1, 39, 80, 0, 114, 0, 101, 0, 115, 0, 115, 0, 32, 0, 69, 0, 78, 0, 84, 0, 69, 0, 82, 0, 32, 0, 116, 0, 111, 0, 32, 0, 101, 0, 120, 0, 105, 0, 116, 0, 0, 41, 78, 0, 117, 0, 107, 0, 101, 0, 85, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 114, 0, 32, 0, 86, 0, 101, 0, 114, 0, 115, 0, 105, 0, 111, 0, 110, 0, 32, 0, 0, 5, 70, 0, 50, 0, 0, 85, 68, 0, 101, 0, 116, 0, 101, 0, 99, 0, 116, 0, 101, 0, 100, 0, 32, 0, 97, 0, 110, 0, 32, 0, 117, 0, 110, 0, 102, 0, 105, 0, 110, 0, 105, 0, 115, 0, 104, 0, 101, 0, 100, 0, 32, 0, 117, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 32, 0, 102, 0, 111, 0, 114, 0, 32, 0, 118, 0, 101, 0, 114, 0, 115, 0, 105, 0, 111, 0, 110, 0, 32, 0, 0, 43, 67, 0, 111, 0, 110, 0, 116, 0, 97, 0, 99, 0, 116, 0, 105, 0, 110, 0, 103, 0, 32, 0, 115, 0, 101, 0, 114, 0, 118, 0, 101, 0, 114, 0, 46, 0, 46, 0, 46, 0, 46, 0, 0, 75, 83, 0, 101, 0, 114, 0, 118, 0, 101, 0, 114, 0, 32, 0, 118, 0, 101, 0, 114, 0, 115, 0, 105, 0, 111, 0, 110, 0, 32, 0, 101, 0, 113, 0, 117, 0, 97, 0, 108, 0, 32, 0, 116, 0, 111, 0, 32, 0, 108, 0, 111, 0, 99, 0, 97, 0, 108, 0, 32, 0, 118, 0, 101, 0, 114, 0, 115, 0, 105, 0, 111, 0, 110, 0, 0, 107, 82, 0, 117, 0, 110, 0, 32, 0, 119, 0, 105, 0, 116, 0, 104, 0, 32, 0, 102, 0, 111, 0, 114, 0, 99, 0, 101, 0, 32, 0, 97, 0, 114, 0, 103, 0, 117, 0, 109, 0, 101, 0, 110, 0, 116, 0, 32, 0, 116, 0, 111, 0, 32, 0, 102, 0, 111, 0, 114, 0, 99, 0, 101, 0, 32, 0, 97, 0, 110, 0, 32, 0, 117, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 32, 0, 105, 0, 102, 0, 32, 0, 100, 0, 101, 0, 115, 0, 105, 0, 114, 0, 101, 0, 100, 0, 0, 0, 0, 0, 242, 25, 10, 192, 14, 88, 179, 64, 165, 113, 90, 51, 214, 216, 5, 109, 0, 8, 183, 122, 92, 86, 25, 52, 224, 137, 2, 6, 13, 5, 0, 1, 1, 29, 14, 3, 32, 0, 1, 4, 32, 1, 1, 14, 4, 32, 1, 1, 2, 5, 32, 1, 1, 17, 65, 4, 32, 1, 1, 8, 5, 0, 2, 2, 14, 14, 4, 0, 0, 18, 81, 3, 32, 0, 14, 4, 0, 1, 14, 14, 2, 6, 14, 5, 0, 2, 14, 14, 14, 4, 0, 1, 2, 14, 4, 0, 1, 1, 14, 3, 0, 0, 1, 3, 0, 0, 14, 8, 48, 173, 79, 230, 178, 166, 174, 237, 6, 16, 1, 1, 30, 0, 14, 4, 10, 1, 18, 89, 4, 32, 1, 14, 14, 3, 32, 0, 2, 3, 32, 0, 8, 5, 0, 2, 14, 28, 28, 9, 32, 1, 21, 18, 113, 1, 18, 117, 8, 6, 21, 18, 113, 1, 18, 117, 4, 32, 0, 19, 0, 5, 32, 1, 1, 18, 117, 7, 32, 2, 1, 18, 117, 18, 117, 8, 32, 0, 21, 18, 113, 1, 18, 89, 6, 21, 18, 113, 1, 18, 89, 10, 32, 1, 21, 18, 113, 1, 18, 117, 18, 89, 15, 7, 8, 2, 14, 14, 18, 89, 18, 117, 18, 89, 18, 117, 18, 117, 73, 1, 0, 26, 46, 78, 69, 84, 70, 114, 97, 109, 101, 119, 111, 114, 107, 44, 86, 101, 114, 115, 105, 111, 110, 61, 118, 52, 46, 53, 1, 0, 84, 14, 20, 70, 114, 97, 109, 101, 119, 111, 114, 107, 68, 105, 115, 112, 108, 97, 121, 78, 97, 109, 101, 18, 46, 78, 69, 84, 32, 70, 114, 97, 109, 101, 119, 111, 114, 107, 32, 52, 46, 53, 20, 1, 0, 15, 78, 117, 107, 101, 85, 112, 100, 97, 116, 101, 114, 46, 65, 112, 112, 0, 0, 5, 1, 0, 0, 0, 0, 23, 1, 0, 18, 67, 111, 112, 121, 114, 105, 103, 104, 116, 32, 194, 169, 32, 32, 50, 48, 49, 54, 0, 0, 41, 1, 0, 36, 53, 53, 49, 97, 97, 55, 50, 97, 45, 56, 101, 100, 48, 45, 52, 102, 51, 99, 45, 97, 100, 49, 49, 45, 98, 51, 98, 55, 49, 57, 48, 99, 54, 56, 48, 49, 0, 0, 12, 1, 0, 7, 49, 46, 48, 46, 48, 46, 48, 0, 0, 8, 1, 0, 2, 0, 0, 0, 0, 0, 8, 1, 0, 8, 0, 0, 0, 0, 0, 30, 1, 0, 1, 0, 84, 2, 22, 87, 114, 97, 112, 78, 111, 110, 69, 120, 99, 101, 112, 116, 105, 111, 110, 84, 104, 114, 111, 119, 115, 1, 0, 0, 0, 0, 0, 76, 220, 4, 87, 0, 0, 0, 0, 2, 0, 0, 0, 28, 1, 0, 0, 80, 46, 0, 0, 80, 16, 0, 0, 82, 83, 68, 83, 158, 139, 148, 64, 12, 202, 105, 66, 179, 255, 19, 2, 150, 200, 47, 45, 2, 0, 0, 0, 102, 58, 92, 68, 101, 118, 92, 110, 117, 107, 101, 117, 112, 100, 97, 116, 101, 114, 92, 78, 117, 107, 101, 85, 112, 100, 97, 116, 101, 114, 92, 78, 117, 107, 101, 85, 112, 100, 97, 116, 101, 114, 46, 65, 112, 112, 92, 111, 98, 106, 92, 82, 101, 108, 101, 97, 115, 101, 92, 78, 117, 107, 101, 85, 112, 100, 97, 116, 101, 114, 46, 65, 112, 112, 46, 112, 100, 98, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 148, 47, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 174, 47, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 160, 47, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 95, 67, 111, 114, 69, 120, 101, 77, 97, 105, 110, 0, 109, 115, 99, 111, 114, 101, 101, 46, 100, 108, 108, 0, 0, 0, 0, 0, 255, 37, 0, 32, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 16, 0, 0, 0, 32, 0, 0, 128, 24, 0, 0, 0, 56, 0, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 80, 0, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 104, 0, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 144, 0, 0, 0, 160, 64, 0, 0, 224, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 67, 0, 0, 234, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 2, 52, 0, 0, 0, 86, 0, 83, 0, 95, 0, 86, 0, 69, 0, 82, 0, 83, 0, 73, 0, 79, 0, 78, 0, 95, 0, 73, 0, 78, 0, 70, 0, 79, 0, 0, 0, 0, 0, 189, 4, 239, 254, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 63, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 68, 0, 0, 0, 1, 0, 86, 0, 97, 0, 114, 0, 70, 0, 105, 0, 108, 0, 101, 0, 73, 0, 110, 0, 102, 0, 111, 0, 0, 0, 0, 0, 36, 0, 4, 0, 0, 0, 84, 0, 114, 0, 97, 0, 110, 0, 115, 0, 108, 0, 97, 0, 116, 0, 105, 0, 111, 0, 110, 0, 0, 0, 0, 0, 0, 0, 176, 4, 64, 2, 0, 0, 1, 0, 83, 0, 116, 0, 114, 0, 105, 0, 110, 0, 103, 0, 70, 0, 105, 0, 108, 0, 101, 0, 73, 0, 110, 0, 102, 0, 111, 0, 0, 0, 28, 2, 0, 0, 1, 0, 48, 0, 48, 0, 48, 0, 48, 0, 48, 0, 52, 0, 98, 0, 48, 0, 0, 0, 72, 0, 16, 0, 1, 0, 70, 0, 105, 0, 108, 0, 101, 0, 68, 0, 101, 0, 115, 0, 99, 0, 114, 0, 105, 0, 112, 0, 116, 0, 105, 0, 111, 0, 110, 0, 0, 0, 0, 0, 78, 0, 117, 0, 107, 0, 101, 0, 85, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 114, 0, 46, 0, 65, 0, 112, 0, 112, 0, 0, 0, 48, 0, 8, 0, 1, 0, 70, 0, 105, 0, 108, 0, 101, 0, 86, 0, 101, 0, 114, 0, 115, 0, 105, 0, 111, 0, 110, 0, 0, 0, 0, 0, 49, 0, 46, 0, 48, 0, 46, 0, 48, 0, 46, 0, 48, 0, 0, 0, 72, 0, 20, 0, 1, 0, 73, 0, 110, 0, 116, 0, 101, 0, 114, 0, 110, 0, 97, 0, 108, 0, 78, 0, 97, 0, 109, 0, 101, 0, 0, 0, 78, 0, 117, 0, 107, 0, 101, 0, 85, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 114, 0, 46, 0, 65, 0, 112, 0, 112, 0, 46, 0, 101, 0, 120, 0, 101, 0, 0, 0, 72, 0, 18, 0, 1, 0, 76, 0, 101, 0, 103, 0, 97, 0, 108, 0, 67, 0, 111, 0, 112, 0, 121, 0, 114, 0, 105, 0, 103, 0, 104, 0, 116, 0, 0, 0, 67, 0, 111, 0, 112, 0, 121, 0, 114, 0, 105, 0, 103, 0, 104, 0, 116, 0, 32, 0, 169, 0, 32, 0, 32, 0, 50, 0, 48, 0, 49, 0, 54, 0, 0, 0, 80, 0, 20, 0, 1, 0, 79, 0, 114, 0, 105, 0, 103, 0, 105, 0, 110, 0, 97, 0, 108, 0, 70, 0, 105, 0, 108, 0, 101, 0, 110, 0, 97, 0, 109, 0, 101, 0, 0, 0, 78, 0, 117, 0, 107, 0, 101, 0, 85, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 114, 0, 46, 0, 65, 0, 112, 0, 112, 0, 46, 0, 101, 0, 120, 0, 101, 0, 0, 0, 64, 0, 16, 0, 1, 0, 80, 0, 114, 0, 111, 0, 100, 0, 117, 0, 99, 0, 116, 0, 78, 0, 97, 0, 109, 0, 101, 0, 0, 0, 0, 0, 78, 0, 117, 0, 107, 0, 101, 0, 85, 0, 112, 0, 100, 0, 97, 0, 116, 0, 101, 0, 114, 0, 46, 0, 65, 0, 112, 0, 112, 0, 0, 0, 52, 0, 8, 0, 1, 0, 80, 0, 114, 0, 111, 0, 100, 0, 117, 0, 99, 0, 116, 0, 86, 0, 101, 0, 114, 0, 115, 0, 105, 0, 111, 0, 110, 0, 0, 0, 49, 0, 46, 0, 48, 0, 46, 0, 48, 0, 46, 0, 48, 0, 0, 0, 56, 0, 8, 0, 1, 0, 65, 0, 115, 0, 115, 0, 101, 0, 109, 0, 98, 0, 108, 0, 121, 0, 32, 0, 86, 0, 101, 0, 114, 0, 115, 0, 105, 0, 111, 0, 110, 0, 0, 0, 49, 0, 46, 0, 48, 0, 46, 0, 48, 0, 46, 0, 48, 0, 0, 0, 239, 187, 191, 60, 63, 120, 109, 108, 32, 118, 101, 114, 115, 105, 111, 110, 61, 34, 49, 46, 48, 34, 32, 101, 110, 99, 111, 100, 105, 110, 103, 61, 34, 85, 84, 70, 45, 56, 34, 32, 115, 116, 97, 110, 100, 97, 108, 111, 110, 101, 61, 34, 121, 101, 115, 34, 63, 62, 13, 10, 60, 97, 115, 115, 101, 109, 98, 108, 121, 32, 120, 109, 108, 110, 115, 61, 34, 117, 114, 110, 58, 115, 99, 104, 101, 109, 97, 115, 45, 109, 105, 99, 114, 111, 115, 111, 102, 116, 45, 99, 111, 109, 58, 97, 115, 109, 46, 118, 49, 34, 32, 109, 97, 110, 105, 102, 101, 115, 116, 86, 101, 114, 115, 105, 111, 110, 61, 34, 49, 46, 48, 34, 62, 13, 10, 32, 32, 60, 97, 115, 115, 101, 109, 98, 108, 121, 73, 100, 101, 110, 116, 105, 116, 121, 32, 118, 101, 114, 115, 105, 111, 110, 61, 34, 49, 46, 48, 46, 48, 46, 48, 34, 32, 110, 97, 109, 101, 61, 34, 77, 121, 65, 112, 112, 108, 105, 99, 97, 116, 105, 111, 110, 46, 97, 112, 112, 34, 47, 62, 13, 10, 32, 32, 60, 116, 114, 117, 115, 116, 73, 110, 102, 111, 32, 120, 109, 108, 110, 115, 61, 34, 117, 114, 110, 58, 115, 99, 104, 101, 109, 97, 115, 45, 109, 105, 99, 114, 111, 115, 111, 102, 116, 45, 99, 111, 109, 58, 97, 115, 109, 46, 118, 50, 34, 62, 13, 10, 32, 32, 32, 32, 60, 115, 101, 99, 117, 114, 105, 116, 121, 62, 13, 10, 32, 32, 32, 32, 32, 32, 60, 114, 101, 113, 117, 101, 115, 116, 101, 100, 80, 114, 105, 118, 105, 108, 101, 103, 101, 115, 32, 120, 109, 108, 110, 115, 61, 34, 117, 114, 110, 58, 115, 99, 104, 101, 109, 97, 115, 45, 109, 105, 99, 114, 111, 115, 111, 102, 116, 45, 99, 111, 109, 58, 97, 115, 109, 46, 118, 51, 34, 62, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 60, 114, 101, 113, 117, 101, 115, 116, 101, 100, 69, 120, 101, 99, 117, 116, 105, 111, 110, 76, 101, 118, 101, 108, 32, 108, 101, 118, 101, 108, 61, 34, 97, 115, 73, 110, 118, 111, 107, 101, 114, 34, 32, 117, 105, 65, 99, 99, 101, 115, 115, 61, 34, 102, 97, 108, 115, 101, 34, 47, 62, 13, 10, 32, 32, 32, 32, 32, 32, 60, 47, 114, 101, 113, 117, 101, 115, 116, 101, 100, 80, 114, 105, 118, 105, 108, 101, 103, 101, 115, 62, 13, 10, 32, 32, 32, 32, 60, 47, 115, 101, 99, 117, 114, 105, 116, 121, 62, 13, 10, 32, 32, 60, 47, 116, 114, 117, 115, 116, 73, 110, 102, 111, 62, 13, 10, 60, 47, 97, 115, 115, 101, 109, 98, 108, 121, 62, 13, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 12, 0, 0, 0, 192, 63, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
#endif


        public static readonly string JsonFormat = ".json";

        public static readonly string NukeName = "nuke";
        public static readonly string VersionName = "Version";
        public static readonly string VersionsPath = "Versions";
        public static readonly string ProjectInfoFile = "ProjectInfo.json";

        public string Name { get; set; }
        public int Latest { get; set; }
        public string ServerUrl { get; set; }
        public bool FinishedUpdate { get; set; }

        [JsonIgnore]
        public bool Created { get; private set; }

        [JsonIgnore]
        public string Root { get; private set; }

        private CultureInfo c;

        public ProjectInfo()
        {
        }

        private string nukeDir;
        private string versionsDir;
        private bool isClient;

        public static void MakeDefault(string name, string server)
        {
            if (!server.EndsWith("/"))
            {
                server = server + '/';
            }

            ProjectInfo proj = new ProjectInfo();
            proj.Latest = -1;
            proj.ServerUrl = server;
            proj.Name = name;
            string loc = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            proj.InitializeClient(loc);
            proj.Save();

#if EMBED
            string exePath = Path.Combine(loc, name + ".exe");
            File.WriteAllBytes(exePath, EXE_DATA);
#endif
        }

        public void InitializeClient(string root)
        {
            isClient = true;
            c = CultureInfo.InvariantCulture;

            Root = root;
            nukeDir = root;
            Created = true;
        }

        public void InitializeServer(string root)
        {
            isClient = false;
            c = CultureInfo.InvariantCulture;

            //if (root.Contains(NukeName))
            //{
            //    // dipshit, you're not supposed to select a project folder
            //    // but we'll work with it
            //    int index = root.IndexOf(NukeName);
            //    root = root.Remove(index, root.Length - index);
            //}

            Root = root;
            nukeDir = Path.Combine(root, NukeName);
            versionsDir = Path.Combine(nukeDir, VersionsPath);
            Created = Directory.Exists(nukeDir);

            if (Created)
            {
                // see latest
                FileInfo[] files = new DirectoryInfo(versionsDir).GetFiles();
                int[] filesNumbers = new int[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    filesNumbers[i] = int.Parse(Path.GetFileNameWithoutExtension(files[i].Name).Remove(0, VersionName.Length));
                }
                Latest = filesNumbers.Max();
            }
        }

        public async Task<ProjectInfo> GetProjectFromServer()
        {
            using (WebClient client = new WebClient())
            {
                string url = ServerUrl + "/" + ProjectInfoFile;
                string json = await client.DownloadStringTaskAsync(url);
                ProjectInfo info = JsonConvert.DeserializeObject<ProjectInfo>(json);
                return info;
            }
        }

        public async Task<UpdateInfo> GetLatestVersionFromServer(ProjectInfo serverVersion)
        {
            return await GetVersionFromServer(serverVersion.Latest);
        }
        public async Task<UpdateInfo> GetVersionFromServer(int version)
        {
            using (WebClient client = new WebClient())
            {
                string url = ServerUrl + "/" + VersionsPath + "/" + VersionName + version.ToString(c) + JsonFormat;
                string json = await client.DownloadStringTaskAsync(url);
                UpdateInfo info = JsonConvert.DeserializeObject<UpdateInfo>(json);
                return info;
            }
        }


        public UpdateInfo ReadUpdate(int revision)
        {
            string path = Path.Combine(versionsDir, VersionName + revision.ToString(CultureInfo.InvariantCulture) + JsonFormat);
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<UpdateInfo>(json);
        }

        public void DownloadUpdateFromServer(UpdateInfo update)
        {
            string updateDir = Path.Combine(Root, "Update");
            if (Directory.Exists(updateDir))
            {
                Directory.Delete(updateDir, true);
            }
            Directory.CreateDirectory(updateDir);

            string rootUrl = ServerUrl + VersionsPath + "/" + VersionName + update.Revision.ToString(c) + "/";

            for (int i = 0; i < update.Entries.Count; i++)
            {
                EntryInfo entry = update.Entries[i];

                if (entry.Type == EntryType.Directory)
                {
                    continue;
                }

                if (entry.State == EntryState.Added ||
                    entry.State == EntryState.Updated)
                {
                    string relPath = Path.Combine(entry.RelativePath, entry.Name);

                    // check the local file
                    string localPath = Path.Combine(Root, relPath);

                    if (File.Exists(localPath))
                    {
                        using (Stream inStream = File.OpenRead(localPath))
                        {
                            using (var md5 = new MD5CryptoServiceProvider())
                            {
                                var buffer = md5.ComputeHash(inStream);
                                var sb = new StringBuilder();
                                for (int j = 0; j < buffer.Length; j++)
                                {
                                    sb.Append(buffer[j].ToString("x2"));
                                }
                                string hash = sb.ToString();
                                if (hash == entry.Hash)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    string to = Path.Combine(updateDir, relPath);
                    string dir = Path.GetDirectoryName(to);
                    Directory.CreateDirectory(dir);

                    using (WebClient client = new WebClient())
                    {
                        Console.WriteLine("Downloading " + entry.Name);
                        string url = rootUrl + relPath.Replace(Path.DirectorySeparatorChar, '/');
                        client.DownloadFile(url, to);
                    }
                }
            }
        }

        /// <summary>
        /// This method expects that you already downloaded the entire update
        /// </summary>
        /// <param name="local"></param>
        /// <param name="server"></param>
        public void DoUpdateFromServer(UpdateInfo local, UpdateInfo update)
        {
            string updateDir = Path.Combine(Root, "Update");

            for (int i = 0; i < update.Entries.Count; i++)
            {
                EntryInfo entry = update.Entries[i];

                string relPath = Path.Combine(entry.RelativePath, entry.Name);
                string to = Path.Combine(updateDir, relPath);

                if (entry.Type == EntryType.Directory)
                {
                    Directory.CreateDirectory(to);
                    continue;
                }

                if (entry.State == EntryState.Added ||
                    entry.State == EntryState.Updated)
                {
                    if (!File.Exists(to))
                    {
                        // file was already here
                        continue;
                    }

                    string rooted = Path.Combine(Root, relPath);
                    string dir = Path.GetDirectoryName(to);
                    Directory.CreateDirectory(dir);

                    File.Move(to, rooted);
                }
                else if (entry.State == EntryState.Removed)
                {
                    // deleeeeete
                    if (File.Exists(to))
                    {
                        File.Delete(to);
                    }
                }
            }

            Directory.Delete(updateDir, true);
        }

        public void Make()
        {
            Directory.CreateDirectory(nukeDir);
            DirectoryInfo dir = new DirectoryInfo(nukeDir);
            dir.Attributes = FileAttributes.Hidden;

            Directory.CreateDirectory(versionsDir);
        }

        public void SaveUpdate(UpdateInfo update)
        {
            Latest = Math.Max(Latest, update.Revision);

            string path = Path.Combine(versionsDir, VersionName + update.Revision.ToString(CultureInfo.InvariantCulture) + JsonFormat);
            File.WriteAllText(path, JsonConvert.SerializeObject(update));

            // make directory
            string updateDir = Path.Combine(versionsDir, VersionName + update.Revision.ToString(CultureInfo.InvariantCulture));
            Directory.CreateDirectory(updateDir);

            for (int i = 0; i < update.Entries.Count; i++)
            {
                EntryInfo entry = update.Entries[i];
                if (entry.Type == EntryType.File)
                {
                    if (entry.State == EntryState.Added ||
                        entry.State == EntryState.Updated)
                    {
                        string relPath = Path.Combine(entry.RelativePath, entry.Name);
                        string from = Path.Combine(Root, relPath);
                        string to = Path.Combine(updateDir, relPath);
                        string dir = Path.GetDirectoryName(to);
                        Directory.CreateDirectory(dir);

                        File.Copy(from, to);
                    }
                }
            }
        }

        public void Save()
        {
            string file = Path.Combine(nukeDir, ProjectInfoFile);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }

        public void Save(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this));
        }
    }
}
