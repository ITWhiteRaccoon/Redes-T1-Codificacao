﻿using System.Text;

namespace Decodificador;

public class Decoder
{
    private static readonly Dictionary<string, char> BinToHexChar = new()
    {
        { "0000", '0' },
        { "0001", '1' },
        { "0010", '2' },
        { "0011", '3' },
        { "0100", '4' },
        { "0101", '5' },
        { "0110", '6' },
        { "0111", '7' },
        { "1000", '8' },
        { "1001", '9' },
        { "1010", 'a' },
        { "1011", 'b' },
        { "1100", 'c' },
        { "1101", 'd' },
        { "1110", 'e' },
        { "1111", 'f' }
    };

    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("erro");
            return;
        }

        try
        {
            string signalInput = args[1].ToLower();
            switch (args[0].ToLower())
            {
                case "nrzi":
                    Console.WriteLine(DecodeNrzi(signalInput));
                    break;
                case "mdif":
                    Console.WriteLine(DecodeMdif(signalInput));
                    break;
                case "hdb3":
                    Console.WriteLine(DecodeHdb3(signalInput));
                    break;
                case "8b6t":
                    Console.WriteLine(Decode8B6T(signalInput));
                    break;
                case "6b8b":
                    Console.WriteLine(Decode6B8B(signalInput));
                    break;
                default:
                    Console.WriteLine("erro");
                    break;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("erro");
        }
    }

    public static string BinToHex(string bin)
    {
        StringBuilder hex = new();
        bin = bin.PadLeft((int)Math.Ceiling((double)(bin.Length / 4)), '0');
        for (int i = 0; i < bin.Length; i += 4)
        {
            hex.Append(BinToHexChar[bin[i..(i + 4)]]);
        }

        return hex.ToString();
    }

    public static string DecodeNrzi(string signalInput)
    {
        StringBuilder decodedDataBin = new();
        char lastSignal = '-';
        foreach (char c in signalInput)
        {
            //Para cada sinal lido, se diferente do anterior quer dizer que o bit é 1, se igual então 0
            int digit = c != lastSignal ? 1 : 0;
            //Guarda o sinal lido como último usado
            lastSignal = c;
            //Adiciona o bit ao final dos dados decodificados
            decodedDataBin.Append(digit);
        }

        //O dado foi lido bit a bit. Para retornar, transforma na representação em hexa
        return BinToHex(decodedDataBin.ToString()).ToUpper();
    }

    public static string DecodeMdif(string signalInput)
    {
        StringBuilder decodedDataBin = new();
        char lastSignal = '-';
        for (int i = 0; i < signalInput.Length; i += 2)
        {
            int digit = signalInput[i] != lastSignal ? 0 : 1;
            lastSignal = signalInput[i + 1];
            decodedDataBin.Append(digit);
        }

        return BinToHex(decodedDataBin.ToString()).ToUpper();
    }

    public static string DecodeHdb3(string signalInput)
    {
        return "";
    }

    public static string Decode8B6T(string signalInput)
    {
        return "";
    }

    public static string Decode6B8B(string signalInput)
    {
        return "";
    }
}