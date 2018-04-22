using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quicksilver.Filesystem.Praxis
{
    unsafe class UnsignedWriter
    {
        byte* ptr;
        public UnsignedWriter(byte* pointer)
        {
            ptr = pointer;
        }
        public void Write64(ulong u) {
            *((ulong*)ptr) = *((ulong*)u);
            ptr += 8;
        }
        public void Write32(uint i) {
            *((uint*)ptr) = *((uint*)i);
            ptr += 4;
        }
        public void Write16(ushort s) {
            *((ushort*)ptr) = *((ushort*)s);
            ptr += 2;
        }
        public void Write8(byte b) {
            *((byte*)ptr) = *((byte*)b);
            ptr += 1;
        }
        public void Write8AtOffset(byte b, uint offset) {
            *((byte*)ptr + offset) = *((byte*)b);
        }
        public void Write16AtOffset(ushort s, uint offset) {
            *((ushort*)ptr + offset) = *((ushort*)s);
        }
        public void Write32AtOffset(uint u, uint offset)
        {
            *((uint*)ptr + offset) = *((uint*)u);
        }
        public void Write64AtOffset(ulong u, uint offset)
        {
            *((ulong*)ptr + offset) = *((ulong*)u);
        }
        public void Write(byte[] par0) {
            fixed(byte* ptra = par0) {
                *((byte*)ptr) = *(ptra);
                ptr += par0.Length;
            }
        }
        public void Advance(uint length) {
            ptr += length;
        }
    }
    unsafe class UnsignedReader
    {
        byte* ptr;
        public UnsignedReader(byte* pointer) { ptr = pointer; }
        public void Advance(uint i) {
            ptr += i;
        }
        public ulong Read64() {
            ptr += 8;
            return *((ulong*)ptr - 8);
        }
        public uint Read32() {
            ptr += 4;
            return *((uint*)ptr - 4);
        }
        public ushort Read16() {
            ptr += 2;
            return *((ushort*)ptr - 2);
        }
        public byte Read8() {
            ptr++;
            return *((byte*)ptr - 1);
        }
    }
}
